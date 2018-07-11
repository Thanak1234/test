using System;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories.GMU
{
    public class GmuRamClearRepository : RepositoryBase<GmuRamClear>, IGmuRamClearRepository
    {
        public GmuRamClearRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public GmuRamClear GetByRequestHeader(int id)
        {
            IDbSet<GmuRamClear> dbSet = DbContext.Set<GmuRamClear>();
            try
            {
                return dbSet.Single(p => p.RequestHeaderId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
    }
}
