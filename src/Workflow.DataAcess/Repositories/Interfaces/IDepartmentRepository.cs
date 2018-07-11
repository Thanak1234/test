/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<DeptLookup> GetDepartments();
        DeptLookup GetDepartment(int Id);
    }
}
