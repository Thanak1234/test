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
    public class ProjectDetailRepository : RepositoryBase<ProjectDetail>, IProjectDetailRepository
    {
        public ProjectDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public ProjectDetail GetByRequestHeader(int id)
        {
            IDbSet<ProjectDetail> dbSet = DbContext.Set<ProjectDetail>();
            try
            {
                return dbSet.Single(p => p.RequestHeaderId == id);
            }
            catch(Exception)
            {
                return null;
            }
            
        }

        public IEnumerable<CapexCategory> GetCapexCategories()
        {
            IDbSet<CapexCategory> dbSet = DbContext.Set<CapexCategory>();
            try
            {
                return dbSet.ToList();
            }
            catch (SmartException)
            {
                return null;
            }
        }
    }
}
