using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.RoleRights;
using Workflow.DataObject.DepartmentRight;
using Workflow.Domain.Entities.Core;

namespace Workflow.DataAcess.Repositories.DepartmentRight
{
    public interface IDeptRightAssignmentRepository : IRepository<DeptAccessRight>
    {
        DeptAccessRight GetDeptAccessRightById(int id);
        IEnumerable<FormDto> GetForms();
        IEnumerable<DepartmentRightDto> GetDepartments();
        IEnumerable<DepartmentRightDto> GetDepartments(int id);
        IEnumerable<DepartmentRightDto> GetDepartments(string query); //query to search based on Department Name or Department Code
        IEnumerable<DeptAccessRightDto> GetDeptAccessRight(int FormId, int DeptId);

        void RoleManagementSync();

    }
}
