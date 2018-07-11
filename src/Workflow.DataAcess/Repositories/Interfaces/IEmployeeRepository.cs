/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Scheduler;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Repositories.Interfaces
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        int CountEmployee(DataObject.QueryParameter param);
        int IsEmployeeExisted(string empNo);
        IEnumerable<EmpNoDeptDto> GetEmployeesNoDept();
    }
}
