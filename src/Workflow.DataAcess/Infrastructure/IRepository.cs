/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataAcess.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Creates a new instance of an entity of type {T}
        /// </summary>
        /// <returns>The new entity instance.</returns>
        T Create();

        void Commit();
        // Marks an entity as new
        void Add(T entity);
        // Marks an entity as modified
        void Update(T entity);
        // Marks an entity to be removed
        void Delete(T entity);

        void InsertRange(IEnumerable<T> entities, int batchSize = 100);

        void DeleteRange(IEnumerable<T> entities);

        void Delete(Expression<Func<T, bool>> where);
        // Get an entity by int id
        T GetById(int id);
        // Get an entity using delegate
        T Get(Expression<Func<T, bool>> where);
        // Gets all entities of type T
        IEnumerable<T> GetAll();
        // Gets entities using delegate
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        IEnumerable<TEntity> SqlQuery<TEntity>(string commandText, object parameters, bool attachDbSet = false) where TEntity : class;
        IEnumerable<TEntity> SqlQuery<TEntity>(string commandText, bool attachDbSet = false) where TEntity : class;
        IEnumerable<TEntity> SqlQuery<TEntity>(string commandText, params object[] parameters) where TEntity : class;
        T Single(string commandText, object parameters);
        TEntity Single<TEntity>(string commandText, object[] parameters) where TEntity : class;


       IEnumerable<T> SqlQuery(string commandText, object parameter, bool attachDbSet = false);
       IEnumerable<T> SqlQuery(string commandText, bool attachDbSet = false);
       IEnumerable<T> StoreProc(string commandText, object parameter);

        void executeSqlCommand(string sqlText);
    }
}
