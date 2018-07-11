using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BCJ;

namespace Workflow.DataAcess.Repositories.BCJ
{
    public class AnalysisItemRepository : RepositoryBase<AnalysisItem>, IAnalysisItemRepository
    {
        public AnalysisItemRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<AnalysisItem> GetByProjectDetail(int projectDetailId)
        {
            IDbSet<AnalysisItem> dbSet = DbContext.Set<AnalysisItem>();
            try
            {
                return dbSet.Where(p => p.ProjectDetailId == projectDetailId);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
    }
}
