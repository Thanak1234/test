using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.PBF;

namespace Workflow.DataAcess.Repositories.PBF
{
    public class ProjectBriefRepository : RepositoryBase<ProjectBrief>, IProjectBriefRepository
    {
        public ProjectBriefRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public ProjectBrief GetByRequestHeader(int id)
        {
            IDbSet<ProjectBrief> dbSet = DbContext.Set<ProjectBrief>();
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
