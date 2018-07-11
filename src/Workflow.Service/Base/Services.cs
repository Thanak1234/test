using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Workflow.DataAcess;

namespace Workflow.Service
{
    public class Services<T> : IServices<T> where T:class
    {
        public T Update(T entity)
        {
            using (var dbContext = new WorkflowContext())
            {
                var repository = dbContext.Set<T>();
                repository.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            return entity;
        }

        public T SaveCommit(T entity)
        {
            using (var dbContext = new WorkflowContext())
            {
                var repository = dbContext.Set<T>();
                repository.Add(entity);
                dbContext.SaveChanges();
            }
            return entity;
        }

        public IList<T> GetAll()
        {
            using (var dbContext = new WorkflowContext())
            {
                var dbSet = dbContext.Set<T>();
                return dbSet.ToList();
            }
        }

        public IList<T> ExecSqlQuery(string sqlQuery, params object[] parameters)
        {
            using (var dbContext = new WorkflowContext())
            {
                return dbContext.Database.SqlQuery<T>(sqlQuery, parameters).ToList();
            }
        }

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
    }

    public class Services : IServices {
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
    }
}