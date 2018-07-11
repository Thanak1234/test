using System.Collections.Generic;
using System.Web.Http;
using Workflow.Core.Attributes;
using Workflow.DataAcess.Repositories;
using Workflow.DataObject.Employee;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    [RoutePrefix("api/organization")]
    public class OrganizationController : ApiController {

        private Repository _repository;

        public OrganizationController() {
            _repository = new Repository();
        }

        [Route("employees")]
        public object GetEmployees() {
            return _repository.ExecDynamicSqlQuery(
                @"SELECT E.* FROM [HumanResources].Employee E WHERE E.Active = 1 ORDER BY E.[Code] DESC");
        }
        
        [Route("approvers")]
        public object GetApprovers(string type, string ids, string actIds)
        {
            if (type == "EMP")
            {
                string criteria = string.Empty;
                if (!string.IsNullOrEmpty(ids))
                {
                    criteria += " AND A.EMP_ID IN (" + ids + ")";
                }

                if (!string.IsNullOrEmpty(actIds))
                {
                    criteria += " AND A.ACT_ID IN (" + actIds + ")";
                }

                return _repository.ExecDynamicSqlQuery(string.Format(
                    @"SELECT E.* FROM [HR].[EMPLOYEE_APPROVER] A
                INNER JOIN [HumanResources].Employee E ON E.Id = A.APPROVER_ID
                WHERE A.DELETED_DATE IS NULL {0} 
                ORDER BY E.[Code]", criteria));
            } else {
                return _repository.ExecDynamicSqlQuery(string.Format(@"
                SELECT E.* FROM [BPMDATA].[VIEW_USER_ROLE] UR
                INNER JOIN [BPMDATA].[DEPT_APPROVAL_ROLE] R ON R.DEPT_APPROVAL_ROLE = UR.ROLE_NAME
                INNER JOIN [HumanResources].[Employee] E ON E.Account = UR.LOGIN_NAME
                WHERE UR.[ACTIVE] = 1 AND R.ID IN ({0})
                ", ids));
            }
        }

        [Route("available-approvers")]
        public object GetAvailableApprovers(string type, string ids)
        {
            if (type == "EMP")
            {
                /*
                return _repository.ExecDynamicSqlQuery(string.Format(
                    @"SELECT A.* FROM [HumanResources].Employee A 
                    WHERE A.Active = 1 AND (A.[Type] = 'INTEGRATED' OR A.[Email] IS NOT NULL) AND EXISTS(
	                    SELECT TOP 1 1 FROM [HumanResources].Employee E 
	                    WHERE ((E.[LevelNum] <= 3) OR E.TeamId IN(A.TeamId)) AND A.LevelNum <= E.[LevelNum]
	                    AND E.Id IN ({0})
                    )", ids)
                 );
                 */
                return _repository.ExecDynamicSqlQuery(
                   @"SELECT A.* FROM [HumanResources].Employee A 
                    WHERE A.Active = 1 AND (A.[Type] = 'INTEGRATED' OR A.[Email] IS NOT NULL) AND [LevelNum] < 6"
               );
            }
            else {
                return _repository.ExecDynamicSqlQuery(
                    @"SELECT A.* FROM [HumanResources].Employee A 
                    WHERE A.Active = 1 AND (A.[Type] = 'INTEGRATED' OR A.[Email] IS NOT NULL) AND [LevelNum] < 6"
                );
            }
            
        }
        

        [Route("roles")]
        public object GetRoles(string actIds)
        {
            return _repository.ExecDynamicSqlQuery(string.Format(@"
            SELECT R.ID, R.ACT_ID, R.[DEPT_APPROVAL_ROLE] ROLE_ID,  ISNULL(D.FULL_DEPT_NAME, R.[DESCRIPTION]) [NAME] 
            FROM BPMDATA.DEPT_APPROVAL_ROLE R LEFT JOIN [HR].[VIEW_DEPARTMENT] D ON D.TEAM_ID = R.DEPT_ID
            WHERE R.[ACTIVE] = 1 AND R.DEPRECATED = 0 AND R.ACT_ID IN ({0})
            ", actIds));
        }

        [Route("departments")]
        public object GetDepartments()
        {
            return _repository.ExecDynamicSqlQuery(@"SELECT D.TEAM_ID ID, D.* FROM HR.VIEW_DEPARTMENT D");
        }

        [Route("activities")]
        public object GetActivitiesByApp(string appIds) {
            return _repository.ExecDynamicSqlQuery(string.Format(
                @"SELECT A.ID, WORKFLOW_ID APP_ID, APP.PROCESS_NAME , A.DISPLAY_NAME, ACT_CODE 
                FROM [ADMIN].[ACTIVITY] A 
                INNER JOIN [BPMDATA].[REQUEST_APPLICATION] APP ON APP.ID = A.WORKFLOW_ID 
                WHERE A.[ACTIVE] = 1 AND APP.ACTIVE = 1 AND A.WORKFLOW_ID IN ({0}) 
                ORDER BY APP.PROCESS_NAME, A.[SEQUENCE], A.DISPLAY_NAME
            ", string.IsNullOrEmpty(appIds)?"0":appIds));
        }

        [Route("applications")]
        public object GetApplications() {
            return _repository.ExecDynamicSqlQuery(
                @"SELECT ID, REQUEST_CODE, PROCESS_NAME, PROCESS_PATH 
                FROM [BPMDATA].[REQUEST_APPLICATION] WHERE [ACTIVE] = 1
                ORDER BY PROCESS_NAME"
            );
        }

        /* TRANSACTION STATEMENT */
        [HttpPost]
        [Route("save-approver")]
        public IHttpActionResult Post(ApproverConfig configure)
        {
            string actionBy = @"NAGAWORLD\yimsamaune";
            var repository = new Repository<EmployeeApprover>();

            if (configure != null) {
                if (configure.type == "ROLE") {
                    _repository.ExecCommand(string.Format(
                        @"EXEC [BPMDATA].[SAVE_APPROVER_BY_ROLE] @RoleIds = '{0}', @ApproverIds = '{1}', @actionBy = '{2}' ",
                        configure.roleIds, configure.approverIds, actionBy
                    ));
                }
                if (configure.type == "EMP") {
                    _repository.ExecCommand(string.Format(
                        @"EXEC [BPMDATA].[SAVE_APPROVER_BY_USER] @EmpIds = '{0}', @ApproverIds = '{1}', @actIds = '{2}', @actionBy = '{3}'",
                        configure.empIds, configure.approverIds, configure.actIds, actionBy
                    ));
                }
            }

            return Ok(configure);
        }
    }

    public class ApproverConfig {
        public string appIds { get; set; }
        public string actIds { get; set; }
        public string roleIds { get; set; }
        public string empIds { get; set; }
        public string approverIds { get; set; }
        public string type { get; set; }
    }
}