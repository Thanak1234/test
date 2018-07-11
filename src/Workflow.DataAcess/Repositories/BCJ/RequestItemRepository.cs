using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.BCJ;

namespace Workflow.DataAcess.Repositories.BCJ
{
    public class RequestItemRepository : RepositoryBase<BcjRequestItem>, IRequestItemRepository
    {
        public RequestItemRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<BcjRequestItem> GetByProjectDetail(int projectDetailId)
        {
            IDbSet<BcjRequestItem> dbSet = DbContext.Set<BcjRequestItem>();
            try
            {
                return dbSet.Where(p => p.ProjectDetailId == projectDetailId);
            }
            catch(Exception)
            {
                return null;
            }
            
        }
    }
}
