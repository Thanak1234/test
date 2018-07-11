/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.DataObject.Employees;
using Workflow.Domain.Entities;

namespace Workflow.Service.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployeeList();
        IEnumerable<EmployeeDto> SearchEmployee(string value);

        IEnumerable<EmployeeDto> SearchEmployee(EmployeeQueryParameter param);
        int CountEmployee(QueryParameter param);

        IEnumerable<DepartmentDto> GetDepartments();
        IEnumerable<DepartmentDto> SearchDepartment(string value);
        IEnumerable<DepartmentDto> GetDepartmentRightByUser(int userId, string appName);
        EmployeeDto GetEmployee(int id);
        EmployeeDto GetRequestorByRequestHeaderId(int id);
        EmployeeDto GetRequestor(int requestHeaderId, int requestorId);
        EmployeeDto GetEmployeeByNo(string empNo);
        EmployeeDto GetEmpByLoginName(string loginname);
        EmployeeDto getEmplyByEmpNo(string empNo);
        void UpdateEmployeeProfile(Employee obj);
		//DepartmentDto GetDepartment(int? id);

        Employee GetEmployeeRaw(int id);
        int IsEmployeeExisted(string empNo);
        ServiceResponseMsg AddNewManualEmployee(Employee emp);
        ServiceResponseMsg UpdateManualEmployee(Employee emp);
    }
}
