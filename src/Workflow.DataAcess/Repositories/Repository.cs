using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;

namespace Workflow.DataAcess.Repositories
{
    public class Repository<T> : RepositoryBase<T> where T : class
    {
        public Repository() : base(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow)) {

        }

        public IDbSet<T> Entities()
        {
            return DbContext.Set<T>();
        }

        public IList<dynamic> ExecDynamicSqlQuery(string sqlQuery, params object[] parameters)
        {
            return new Repository().ExecDynamicSqlQuery(sqlQuery, parameters);
        }

        public int Single(string sqlQuery, params object[] parameters) {
            return DbContext.Database.SqlQuery<int>(sqlQuery, parameters).FirstOrDefault();
        }
    }

    public class Repository {
        private IList<dynamic> DataCollection { get; set; }
        
        public IList<dynamic> ExecDynamicSqlQuery(string sqlQuery, params object[] parameters)
        {
            using (var dbContext = new WorkflowContext())
            {
                var dataCollection = new List<dynamic>();
                dynamic result = dbContext.Database.DynamicSqlQuery(sqlQuery, parameters);
                foreach (dynamic data in result)
                {
                    dataCollection.Add(data);
                }
                return dataCollection;
            }
        }

        public T ExecSingle<T>(string sqlQuery, params object[] parameters) 
        {
            using (var dbContext = new WorkflowContext())
            {
                var query = dbContext.Database.SqlQuery<T>(sqlQuery, parameters);
                return query.SingleOrDefault();
            }
        }

        public IList<T> ExecSqlQuery<T>(string sqlQuery, params object[] parameters) where T:class
        {
            using (var dbContext = new WorkflowContext())
            {
                var query = dbContext.Database.SqlQuery<T>(sqlQuery, parameters);
                return query.ToList();
            }
        }

        public Repository DynamicQuery(string sqlQuery, params object[] parameters)
        {
            DataCollection = new List<dynamic>();
            using (var dbContext = new WorkflowContext())
            {
                var dataCollection = new List<dynamic>();
                dynamic result = dbContext.Database.DynamicSqlQuery(sqlQuery, parameters);
                foreach (dynamic data in result)
                {
                    dataCollection.Add(data);
                }

                DataCollection = dataCollection;
                return this;
            }
        }

        public dynamic GetDynamicObject(){
            if (DataCollection != null && DataCollection.Count > 0)
            {
                return DataCollection.FirstOrDefault();
            }

            return null;
        }

        public void ExecCommand(string sqlQuery, params object[] parameters)
        {
            using (var dbContext = new WorkflowContext())
            {
                var query = dbContext.Database.ExecuteSqlCommand(sqlQuery, parameters);
            }
        }

    }
}
