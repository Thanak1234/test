using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;

namespace Workflow.Service
{
    public interface IServices
    {
        IList<dynamic> ExecDynamicSqlQuery(string sqlQuery, params object[] parameters);
    }

    public interface IServices<T>: IServices where T:class
    {
        T Update(T entity);
        T SaveCommit(T entity);
        IList<T> GetAll();
        IList<T> ExecSqlQuery(string sqlQuery, params object[] parameters);
    }

    
}