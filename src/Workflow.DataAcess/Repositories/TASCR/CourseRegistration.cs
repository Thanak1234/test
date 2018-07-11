using System;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories
{
    public class CourseRegistrationRepository : RepositoryBase<CourseRegistration>, ICourseRegistrationRepository
    {
        public CourseRegistrationRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public CourseRegistration GetByRequestHeader(int id)
        {
            IDbSet<CourseRegistration> dbSet = DbContext.Set<CourseRegistration>();
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
