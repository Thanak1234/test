/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.AV;

namespace Workflow.DataAcess.Repositories
{
    public class AvbRequestItemRepository : RepositoryBase<AvbRequestItem>, IAvbRequestItemRepository
    {
        public AvbRequestItemRepository (IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<AvbRequestItem> GetByRequestHeader(int id)
        {
            IDbSet<AvbRequestItem> dbSet = DbContext.Set<AvbRequestItem>();
            try
            {
                var result= dbSet.Where(p => p.RequestHeaderId == id).ToList();
                return result;
            }
            catch (SmartException e)
            {
                throw e;
            }
        }
    }
}
