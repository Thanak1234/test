/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Core.Attributes;

namespace Workflow.DataAcess.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {

        #region properties

        private DbContext dataContext;
        private readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected DbSet<T> DbSet
        {
            get { return dbSet;  }
        }

        protected DbContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.init()); }
        }

        #endregion

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();

        }

        #region Implementation

        public T Create()
        {
            return dbSet.Create();
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
            DbContext.SaveChanges();
        }

        public virtual void InsertRange(IEnumerable<T> entities, int batchSize = 100) {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                if (entities.Any())
                {
                    if (batchSize <= 0)
                    {
                        // insert all in one step
                        entities.Each(x => dbSet.Add(x));
                    }
                    else
                    {
                        int i = 1;
                        bool saved = false;
                        foreach (var entity in entities)
                        {
                            dbSet.Add(entity);
                            saved = false;
                            if (i % batchSize == 0)
                            {
                                dataContext.SaveChanges();
                                i = 0;
                                saved = true;
                            }
                            i++;
                        }

                        if (!saved)
                        {
                            dataContext.SaveChanges();
                        }
                    }
                }
            }
            catch (SmartException ex)
            {
                throw ex;
            }
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
            dataContext.SaveChanges();
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            dbSet.RemoveRange(entities);
            DbContext.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);

            dataContext.SaveChanges();
        }

        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IQueryable<T> GetList()
        {
            return dbSet.ToList().AsQueryable();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        /* Execute store procedure */
        public virtual IEnumerable<T> StoreProc(string commandText, object parameter)
        {
            string extraParam = "";
            object[] parameters = this.ToParameters(parameter, ref extraParam);

            if (parameters != null)
            {
                var result = DbContext.Database.SqlQuery<T>(commandText + extraParam, parameters).ToList();
                parameters.Where(p =>
                {
                    var param = p as SqlParameter;
                    if (param.Direction == ParameterDirection.Output)
                    {
                        return true;
                    }
                    return false;
                }).Each(e =>
                {
                    var element = e as SqlParameter;                    

                    parameter.GetType().GetProperties().Each(prop =>
                    {
                        var attribute = prop.GetCustomAttributes(typeof(DataMemberAttribute), true).FirstOrDefault() as DataMemberAttribute;
                        if(attribute != null && attribute.Name.IsCaseSensitiveEqual(element.ParameterName.Replace("@", string.Empty)))
                        {
                            string propName = element.ParameterName.Replace("@", string.Empty);
                            PropertyInfo propertyInfo = parameter.GetType().GetProperty(propName);
                            propertyInfo.SetValue(parameter, Convert.ChangeType(element.Value, propertyInfo.PropertyType), null);
                        }
                    });
                });

                return result;
            }
            else
            {
                var result = DbContext.Database.SqlQuery<T>(commandText + extraParam).ToList();

                return result;
            }
        }

        public virtual T Single(string commandText, object parameters)
        {
            return SqlQuery<T>(commandText, parameters, false).First();
        }
        public virtual IEnumerable<T> SqlQuery(string commandText, bool attachDbSet = false)
        {
            return SqlQuery<T>(commandText, null, attachDbSet);
        }
        public virtual IEnumerable<T> SqlQuery(string commandText, object parameter, bool attachDbSet = false)
        {
            return SqlQuery<T>(commandText, parameter, attachDbSet);
        }
        public virtual IEnumerable<TEntity> SqlQuery<TEntity>(string commandText, bool attachDbSet = false) where TEntity : class
        {
            return SqlQuery<TEntity>(commandText, null, attachDbSet);
        }

        public IEnumerable<TEntity> SqlQuery<TEntity>(string commandText, params object[] parameters) where TEntity : class
        {
            return DbContext.Database.SqlQuery<TEntity>(commandText, parameters).ToList();
        }

        public TEntity Single<TEntity>(string commandText, params object[] parameters) where TEntity : class
        {
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
        public virtual IEnumerable<TEntity> SqlQuery<TEntity>(string commandText, object parameter, bool attachDbSet = false) where TEntity : class
        {
            string extraParam = "";
            object[] parameters = this.ToParameters(parameter,ref extraParam);

            if (parameters != null)
            {
                return DbContext.Database.SqlQuery<TEntity>(commandText, parameters).ToList();
            }
            else
            {
                var result = DbContext.Database.SqlQuery<TEntity>(commandText).ToList();
                //for (int i = 0; i < result.Count; i++)
                //{
                //    result[i] = AttachEntityToContext(result[i]);
                //}
                return result;
            }
        }

        public virtual TNumber GetValue<TNumber>(string commandText, object parameter) where TNumber : struct
        {
            string extraParam = "";
            object[] parameters = this.ToParameters(parameter, ref extraParam);

            if (parameters != null)
            {
                return DbContext.Database.SqlQuery<TNumber>(commandText, parameters).Single();
            }
            else
            {
                var result = DbContext.Database.SqlQuery<TNumber>(commandText).Single();
                return (dynamic)result;
            }
        }

        public virtual string GetText(string commandText, object parameter)
        {
            string extraParam = "";
            object[] parameters = this.ToParameters(parameter, ref extraParam);

            if (parameters != null)
            {
                return DbContext.Database.SqlQuery<string>(commandText, parameters).Single();
            }
            else
            {
                var result = DbContext.Database.SqlQuery<string>(commandText).Single();
                return (dynamic)result;
            }
        }

        private object[] ToParameters(object parameter, ref string extraParam)
        {
            if (parameter == null) { return null; }

            Type parameterType = parameter.GetType();

            // Get all properties for a type
            PropertyInfo[] properties = parameterType.GetProperties();

            object[] parameters = new object[properties.Length];
            int index = 0;
            foreach (PropertyInfo property in properties)
            {
                string paramName = "@" + property.Name;
                var attributes = property.GetCustomAttributes(typeof(DataMemberAttribute), true);
                var customAttribute = property.GetCustomAttributes(typeof(OutputParameterAttribute), true).FirstOrDefault() as OutputParameterAttribute;

                foreach (DataMemberAttribute dma in attributes)
                {
                    paramName = "@" + dma.Name;
                }    

                extraParam += string.Concat(((index == 0)?" ":", ") , paramName , " = " + string.Format("{0}", customAttribute != null? paramName + " OUTPUT": paramName));
                var value = property.GetValue(parameter);

                SqlParameter sqlParameter;
                if(customAttribute != null)
                {
                    sqlParameter = new SqlParameter(paramName, value != null ? value : DBNull.Value);
                    sqlParameter.SqlDbType = GetSqlDbType(customAttribute.ReturnType);
                    sqlParameter.Direction = ParameterDirection.Output;
                } else
                {
                    sqlParameter = new SqlParameter(paramName, value != null ? value : DBNull.Value);
                    sqlParameter.Direction = ParameterDirection.Input;
                }
                parameters.SetValue(sqlParameter, index);
                index++;
            }
            return parameters;
        }

        private SqlDbType GetSqlDbType(Type type)
        {
            SqlDbType returnType = SqlDbType.Int;

            if(type == typeof(int))
            {
                returnType = SqlDbType.Int;
            } else if(type == typeof(string))
            {
                returnType = SqlDbType.VarChar;
            }

            return returnType;
        }

        /// <summary>
        /// Attach an entity to the context
        /// </summary>
        /// <typeparam name="T">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Attached entity</returns>
        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : class
        {
            DbContext.Set<TEntity>().Attach(entity);
            return entity;
        }

        #endregion

        #region DataMember Repository Base

        private IEnumerable<DbParameter> ToParameters(params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                return Enumerable.Empty<DbParameter>();

            return parameters.Cast<DbParameter>();
        }

        #endregion


        public void executeSqlCommand(string sqlText)
        {
            DbContext.Database.ExecuteSqlCommand(sqlText);
        }


    }
}
