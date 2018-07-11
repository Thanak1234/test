using System;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories.JRAM
{
    public class RamClearRepository : RepositoryBase<RamClear>, IRamClearRepository
    {
        public RamClearRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public RamClear GetByRequestHeader(int id)
        {
            IDbSet<RamClear> dbSet = DbContext.Set<RamClear>();
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
