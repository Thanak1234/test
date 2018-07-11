using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BCJ;

namespace Workflow.DataAcess.Repositories.BCJ
{
    public class PurchaseOrderRepository : RepositoryBase<PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<PurchaseOrder> GetByRequestHeader(int id)
        {
            IDbSet<PurchaseOrder> dbSet = DbContext.Set<PurchaseOrder>();
            try
            {
                return dbSet.Where(p => p.RequestHeaderId == id);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
