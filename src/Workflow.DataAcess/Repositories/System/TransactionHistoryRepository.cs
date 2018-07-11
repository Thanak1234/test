using System;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Repositories
{
    public class TransactionHistoryRepository : RepositoryBase<TransactionHistory>, ITransactionHistoryRepository
    {
        public TransactionHistoryRepository() : base(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow))
        {
        
        }
        public TransactionHistoryRepository(IDbFactory dbFactory) : base(dbFactory) {

        }

        public TransactionHistory GetId(int id)
        {
            IDbSet<TransactionHistory> dbSet = DbContext.Set<TransactionHistory>();
            try
            {
                return dbSet.Single(p => p.Id == id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

        //public string GetPendingTreamentMessage()
        //{
        //    try
        //    {
        //        var pendingNumber = SqlQuery<string>(@"EXEC ");
        //        return pendingNumber.Single();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
    }
}
