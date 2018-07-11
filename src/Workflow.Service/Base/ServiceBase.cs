using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess;
using Workflow.DataAcess.Infrastructure;

namespace Workflow.Service {

    public class ServiceBase {

        protected WorkflowContext DbContext { get; set; }

        protected ServiceBase() {
            DbContext = (WorkflowContext)DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow).init();
        }

        #region Implementation

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class {
            DbContext.Set<TEntity>().Add(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public void InsertRange<TEntity>(IEnumerable<TEntity> entities, int batchSize = 100) where TEntity : class {
            try {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                if (entities.Any()) {
                    if (batchSize <= 0) {                     
                        entities.Each(x => DbContext.Set<TEntity>().Add(x));
                    } else {
                        int i = 1;
                        bool saved = false;
                        foreach (var entity in entities) {
                            DbContext.Set<TEntity>().Add(entity);
                            saved = false;
                            if (i % batchSize == 0) {
                                DbContext.SaveChanges();
                                i = 0;
                                saved = true;
                            }
                            i++;
                        }

                        if (!saved) {
                            DbContext.SaveChanges();
                        }
                    }
                }
            } catch (SmartException ex) {
                throw ex;
            }
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class {
            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class {
            DbContext.Set<TEntity>().Remove(entity);
            DbContext.SaveChanges();
        }

        public void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class {
            if (entities == null)
                throw new ArgumentNullException("entities");

            DbContext.Set<TEntity>().RemoveRange(entities);
            DbContext.SaveChanges();
        }

        public void Delete<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class {
            IEnumerable<TEntity> objects = DbContext.Set<TEntity>().Where(where).AsEnumerable();
            foreach (TEntity obj in objects)
                DbContext.Set<TEntity>().Remove(obj);

            DbContext.SaveChanges();
        }

        public T GetById<T>(int id) where T : class {
            return DbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll<T>() where T : class {
            return DbContext.Set<T>().ToList();
        }

        public IQueryable<T> GetList<T>() where T : class {
            return DbContext.Set<T>().ToList().AsQueryable();
        }

        public IEnumerable<T> GetMany<T>(Expression<Func<T, bool>> where) where T : class {
            return DbContext.Set<T>().Where(where).ToList();
        }

        public T Get<T>(Expression<Func<T, bool>> where) where T : class {
            return DbContext.Set<T>().Where(where).FirstOrDefault<T>();
        }

        /* Execute store procedure */
        public IEnumerable<T> StoreProc<T>(string commandText, object parameter) {
            string extraParam = "";
            object[] parameters = this.ToParameters(parameter, ref extraParam);

            if (parameters != null) {
                return DbContext.Database.SqlQuery<T>(commandText + extraParam, parameters).ToList();
            } else {
                var result = DbContext.Database.SqlQuery<T>(commandText + extraParam).ToList();

                return result;
            }
        }

        public T Single<T>(string commandText, object parameters) where T : class {
            return SqlQuery<T>(commandText, parameters, false).First();
        }

        public IEnumerable<TEntity> SqlQuery<TEntity>(string commandText, params object[] parameters) where TEntity : class {
            return DbContext.Database.SqlQuery<TEntity>(commandText, parameters).ToList();
        }

        public TEntity Single<TEntity>(string commandText, params object[] parameters) where TEntity : class {
            return DbContext.Database.SqlQuery<TEntity>(commandText, parameters).Single();

        }


        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  
        /// The type can be any type that has properties that match the names of the columns returned from the query, 
        /// or can be a simple primitive type. The type does not have to be an entity type. 
        /// The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of object returned by the query.</typeparam>
        /// <param name="commandText">The SQL query string.</param>
        /// <param name="parameter">The parameters to apply to the SQL query string.</param>
        /// <param name="attachDbSet">The parameters to attach new entity to DbSet.</param>
        /// <returns>Result</returns>
        public IEnumerable<TEntity> SqlQuery<TEntity>(string commandText, object parameter, bool attachDbSet = false) where TEntity : class {
            string extraParam = "";
            object[] parameters = this.ToParameters(parameter, ref extraParam);

            if (parameters != null) {
                return DbContext.Database.SqlQuery<TEntity>(commandText, parameters).ToList();
            } else {
                var result = DbContext.Database.SqlQuery<TEntity>(commandText).ToList();
                return result;
            }
        }

        public TNumber GetValue<TNumber>(string commandText, object parameter) where TNumber : struct {
            string extraParam = "";
            object[] parameters = this.ToParameters(parameter, ref extraParam);

            if (parameters != null) {
                return DbContext.Database.SqlQuery<TNumber>(commandText, parameters).Single();
            } else {
                var result = DbContext.Database.SqlQuery<TNumber>(commandText).Single();
                return (dynamic)result;
            }
        }

        public string GetText(string commandText, object parameter) {
            string extraParam = "";
            object[] parameters = this.ToParameters(parameter, ref extraParam);

            if (parameters != null) {
                return DbContext.Database.SqlQuery<string>(commandText, parameters).Single();
            } else {
                var result = DbContext.Database.SqlQuery<string>(commandText).Single();
                return (dynamic)result;
            }
        }

        private object[] ToParameters(object parameter, ref string extraParam) {
            if (parameter == null) { return null; }

            Type parameterType = parameter.GetType();

            // Get all properties for a type
            PropertyInfo[] properties = parameterType.GetProperties();

            object[] parameters = new object[properties.Length];
            int index = 0;
            foreach (PropertyInfo property in properties) {
                string paramName = "@" + property.Name;
                var attributes = property.GetCustomAttributes(typeof(DataMemberAttribute), true);
                foreach (DataMemberAttribute dma in attributes) {
                    paramName = "@" + dma.Name;
                }

                extraParam += string.Concat(((index == 0) ? " " : ", "), paramName, " = " + paramName);

                var value = property.GetValue(parameter);
                SqlParameter sqlParameter = new SqlParameter(paramName, value != null ? value : DBNull.Value);
                parameters.SetValue(sqlParameter, index);
                index++;
            }
            return parameters;
        }

        /// <summary>
        /// Attach an entity to the context
        /// </summary>
        /// <typeparam name="T">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Attached entity</returns>
        protected TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : class {
            DbContext.Set<TEntity>().Attach(entity);
            return entity;
        }

        #endregion

        #region DataMember Repository Base

        private IEnumerable<DbParameter> ToParameters(params object[] parameters) {
            if (parameters == null || parameters.Length == 0)
                return Enumerable.Empty<DbParameter>();

            return parameters.Cast<DbParameter>();
        }

        #endregion
    }
}
