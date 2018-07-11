using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories
{
    public class CourseEmployeeRepository : RepositoryBase<CourseEmployee>, ICourseEmployeeRepository
    {
        public CourseEmployeeRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<CourseEmployee> GetByRequestHeaderId(int id)
        {
            IDbSet<CourseEmployee> dbSet = DbContext.Set<CourseEmployee>();
            try
            {
                return dbSet.Where(p => p.RequestHeaderId == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<CourseEmployee>();
            }
        }
    }
}
