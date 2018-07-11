using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.RoleRights;
using Workflow.DataObject.DepartmentRight;
using Workflow.Domain.Entities.Core;
namespace Workflow.Service.Interfaces
{
    public interface IDepartmentRightService
    {
        DeptAccessRight GetDeptAccessRightById(int id);
        IEnumerable<FormDto> GetForms();
        IEnumerable<DepartmentRightDto> GetDepartments();
        IEnumerable<DepartmentRightDto> GetDepartments(int id);
        IEnumerable<DepartmentRightDto> GetDepartments(string query);
        IEnumerable<DeptAccessRightDto> GetDeptAccessRight(int FormId, int DeptId);
        IEnumerable<DeptAccessRightDto> GetDeptAccessRight(int FormId, int DeptId,string query);
        void Add(DeptAccessRight entity);
        void Update(DeptAccessRight entity);
        void RoleManagementSync();
    }
}
