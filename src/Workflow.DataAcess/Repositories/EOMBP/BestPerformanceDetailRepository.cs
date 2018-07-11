using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Finance;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories
{
    public class BestPerformanceDetailRepository : RepositoryBase<BestPerformanceDetail>, IBestPerformanceDetailRepository
    {
        public BestPerformanceDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<BestPerformanceDetail> GetByRequestHeaderId(int id)
        {
            IDbSet<BestPerformanceDetail> dbSet = DbContext.Set<BestPerformanceDetail>();
            try
            {
                return dbSet.Where(p => p.RequestHeaderId == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<BestPerformanceDetail>();
            }
        }

        public IEnumerable<BestPerformanceDetail> GetByRequestHeaderId(int id, string type)
        {
            var bpList = GetByRequestHeaderId(id);
            if (bpList != null && bpList.Count > 0) {
                return bpList.Where(p => p.Type == type);
            }
            return new List<BestPerformanceDetail>();
        }
    }
}
