using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories
{
    public interface ICourseRegistrationRepository : IRepository<CourseRegistration>
    {
        CourseRegistration GetByRequestHeader(int id);
    }
}
