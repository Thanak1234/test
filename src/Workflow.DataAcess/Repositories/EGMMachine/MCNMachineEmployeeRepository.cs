using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.EGM;

namespace Workflow.DataAcess.Repositories.EGMMachine
{
    public class MCNMachineEmployeeRepository : RepositoryBase<MachineEmployee>,IMCNMachineEmployeeRepository
    {
        public MCNMachineEmployeeRepository(IDbFactory dbFactory) :base(dbFactory)
        {

        }

        public IEnumerable<RequestUserExt> GetMachineEmployeeList(int requestHeaderId)
        {
            IEnumerable<RequestUserExt> requestUser = new List<RequestUserExt>();
            
            string sql = "SELECT mcl.ID Id,mcl.REQUEST_HEADER_ID RequestHeaderId,CAST(mcl.ID AS VARCHAR(100)) EmpId, emp.DISPLAY_NAME EmpName, " +
                         "emp.EMP_NO EmpNo,dep.ID TeamId,dep.DEPT_NAME teamName, emp.JOB_TITLE Position,emp.EMAIL Email,emp.HIRED_DATE HiredDate,emp.REPORT_TO Manager,emp.MOBILE_PHONE Phone, CAST('1' AS INTEGER) Version " +
                         "FROM [EGM].MACHINE_EMPLOYEELIST mcl " +
                         "INNER JOIN HR.EMPLOYEE emp ON mcl.EMPNO = emp.EMP_NO " +
                         "INNER JOIN HR.DEPARTMENT dep ON emp.DEPT_ID = dep.ID " +
                         "WHERE mcl.REQUEST_HEADER_ID = @requestheaderid";

            IEnumerable<RequestUserExt> _MachineEmployeeList = DbContext.Database.SqlQuery<RequestUserExt>(
                                                                sql,
                                                                new object[]
                                                                {
                                                                    new SqlParameter("@requestheaderid", requestHeaderId.ToString())
                                                                }).ToList<RequestUserExt>();

            return _MachineEmployeeList;
        }
    }
}
