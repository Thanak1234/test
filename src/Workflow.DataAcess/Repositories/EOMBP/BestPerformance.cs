using System;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories
{
    public class BestPerformanceRepository : RepositoryBase<BestPerformance>, IBestPerformanceRepository
    {
        public BestPerformanceRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public BestPerformance GetByRequestHeader(int id)
        {
            IDbSet<BestPerformance> dbSet = DbContext.Set<BestPerformance>();
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
