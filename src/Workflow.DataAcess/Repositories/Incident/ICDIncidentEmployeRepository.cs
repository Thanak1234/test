using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Repositories.Incident;
using Workflow.Domain.Entities.INCIDENT;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;
using System.Data.SqlClient;

namespace Workflow.DataAcess.Repositories.Incident
{
    public class ICDIncidentEmployeRepository : RepositoryBase<Workflow.Domain.Entities.INCIDENT.IncidentEmployee>,IICDIncidentEmployeeRepository
    {
        public ICDIncidentEmployeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<RequestUserExt> GetIncidentEmployeeList(int requestHeaderId)
        {
            IEnumerable<RequestUserExt> requestUser = new List<RequestUserExt>();

            //SELECT icl.ID,icl.REQUEST_HEADER_ID,icl.ID EMPID, emp.DISPLAY_NAME EMPNAME,
            //emp.EMP_NO,dep.ID TEAMID, emp.JOB_TITLE,emp.EMAIL,emp.HIRED_DATE,emp.REPORT_TO,emp.MOBILE_PHONE
            //FROM SLOT.INCIDENT_EMPLOYEELIST icl
            //INNER JOIN HR.EMPLOYEE emp ON icl.EMPNO = emp.EMP_NO
            //INNER JOIN HR.DEPARTMENT dep ON emp.DEPT_ID = dep.ID

            string sql = "SELECT icl.ID Id,icl.REQUEST_HEADER_ID RequestHeaderId,CAST(icl.ID AS VARCHAR(100)) EmpId, emp.DISPLAY_NAME EmpName, " +
                         "emp.EMP_NO EmpNo,dep.ID TeamId,dep.DEPT_NAME teamName, emp.JOB_TITLE Position,emp.EMAIL Email,emp.HIRED_DATE HiredDate,emp.REPORT_TO Manager,emp.MOBILE_PHONE Phone, CAST('1' AS INTEGER) Version " +
                         "FROM EGM.INCIDENT_EMPLOYEELIST icl " +
                         "INNER JOIN HR.EMPLOYEE emp ON icl.EMPNO = emp.EMP_NO " +
                         "INNER JOIN HR.DEPARTMENT dep ON emp.DEPT_ID = dep.ID " +
                         "WHERE icl.REQUEST_HEADER_ID = @requestheaderid";

            IEnumerable<RequestUserExt> _IncidentEmployeeList = DbContext.Database.SqlQuery<RequestUserExt>(
                                                                sql, 
                                                                new object[] 
                                                                {
                                                                    new SqlParameter("@requestheaderid", requestHeaderId.ToString())
                                                                }).ToList<RequestUserExt>();

            return _IncidentEmployeeList;
        }
    }
}
