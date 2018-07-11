/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject.Scheduler;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public int CountEmployee(DataObject.QueryParameter param) {
            var sql = " SELECT COUNT(*) FROM [HR].[VIEW_EMPLOYEE_LIST] WHERE EMP_NO like @value or LOGIN_NAME like @value or DISPLAY_NAME like @value or EMAIL like  @value ";
            var value = "%" + param.query + "%";

            return  DbContext.Database.SqlQuery<int>(sql, new object[] { new SqlParameter("@value", value) }).FirstOrDefault<int>();            
        }

        public IEnumerable<EmpNoDeptDto> GetEmployeesNoDept() {
            return SqlQuery<EmpNoDeptDto>(@"SELECT
                                             E.EMP_NO code,
                                             E.DISPLAY_NAME fullName,
                                             E.JOB_TITLE position,
                                             E.EMAIL email,
                                             CSV.GROUP_NAME deptCode,
                                             CSV.TEAM_CODE teamCode,
                                             CSV.DEPT_CODE lineCode,
                                             CSV.TEAM_NAME teamDesc,
                                             CSV.DEPT_NAME lineDesc,
                                             CASE WHEN
	                                             CSV.GROUP_NAME IS NOT NULL OR
	                                             CSV.TEAM_CODE IS NOT NULL OR
	                                             CSV.DEPT_CODE IS NOT NULL OR
	                                             CSV.TEAM_NAME IS NOT NULL OR
                                             CSV.DEPT_NAME IS NOT NULL 
                                             THEN 'NEW DEPARTMENT' 
                                             ELSE 'NO DEPARTMENT' 
                                             END suggestion
                                            FROM HR.EMPLOYEE E 
                                            LEFT JOIN [SYNC].VIRTUAL_ROSTER CSV ON RIGHT('00000'+ CONVERT(VARCHAR,CSV.EMP_NO),6) collate database_default = E.EMP_NO
                                            WHERE E.DEPT_ID = -1 AND E.ACTIVE = 1");
        }

        public int IsEmployeeExisted(string empNo)
        {
            var sql = "SELECT COUNT(*) FROM [HR].EMPLOYEE WHERE EMP_NO IS NOT NULL AND LOWER(RTRIM(LTRIM(EMP_NO))) = LOWER(RTRIM(LTRIM(@EMP_NO)))";
            return DbContext.Database.SqlQuery<int>(sql, new object[] {new SqlParameter("@EMP_NO", empNo) }).FirstOrDefault<int>();
        }
    }
}
