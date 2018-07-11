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
    public class SpecificationRepository : RepositoryBase<Specification>, ISpecificationRepository
    {
        public SpecificationRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<Specification> GetByProjectId(int id)
        {
            IDbSet<Specification> dbSet = DbContext.Set<Specification>();
            try
            {
                return dbSet.Where(p => p.ProjectBriefId == id).ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Specification>();
            }
            
        }

        //public void UpdateReferenceItem(int projectId, string projectRef)
        //{
        //    int length = 0;
        //    foreach (var item in GetByProjectId(projectId)) {
        //        length++;
        //        item.ItemReference = string.Concat(projectRef, "-", length.ToString("0000"));
        //        Update(item);
        //    }
        //}
    }
}
