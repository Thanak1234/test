/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Repositories
{
    public class DepartmentRepositoy : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepositoy(): base(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow))
        {

        }

        public IEnumerable<DeptLookup> GetDepartments()
        {
            return SqlQuery<DeptLookup>(@"SELECT TEAM_ID Id, FULL_DEPT_NAME [Name] FROM[HR].[VIEW_DEPARTMENT] ORDER BY [FULL_DEPT_NAME]");
        }

        public DeptLookup GetDepartment(int Id)
        {
            return SqlQuery<DeptLookup>(@"SELECT TEAM_ID Id, FULL_DEPT_NAME [Name] FROM[HR].[VIEW_DEPARTMENT] WHERE [TEAM_ID] = " + Id)
                .SingleOrDefault();
        }
    }
}
