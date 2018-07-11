using System;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.HumanResource;

namespace Workflow.DataAcess.Repositories.HumanResource
{
    public class RequisitionRepository : RepositoryBase<Requisition>, IRequisitionRepository
    {
        public RequisitionRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public Requisition GetByRequestHeader(int id)
        {
            IDbSet<Requisition> dbSet = DbContext.Set<Requisition>();
            try
            {
                return dbSet.Single(p => p.RequestHeaderId == id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
    }
}
