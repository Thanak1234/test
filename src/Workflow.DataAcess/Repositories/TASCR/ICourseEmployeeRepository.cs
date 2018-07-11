using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories
{
    public interface ICourseEmployeeRepository : IRepository<CourseEmployee>
    {
        IList<CourseEmployee> GetByRequestHeaderId(int id);
    }
}
