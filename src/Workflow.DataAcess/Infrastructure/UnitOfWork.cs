/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataAcess.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private DbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public DbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.init()); }
        }

        public void commit()
        {
            DbContext.SaveChanges();
        }

        public DbContextTransaction begin()
        {
            return DbContext.Database.BeginTransaction();
        }

        public void commit(DbContextTransaction tran)
        {
            DbContext.SaveChanges();
            tran.Commit();
        }

        public void rollBack(DbContextTransaction tran)
        {
            tran.Rollback();
        }
    }
}
