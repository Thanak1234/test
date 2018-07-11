using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket
{
    public class TicketAgentRepository : RepositoryBase<TicketAgent>, ITicketAgentRepository
    {
        public TicketAgentRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<TicketAgentDto> getAgents(string query = null)
        {
            string sqlString = @"SELECT A.[ID] id      
                                ,A.[DESCRIPTION] description
                                ,A.[CREATED_DATE] createdDate
                                ,A.[MODIFIED_DATE] modifiedDate	                            
	                            ,GP.GROUP_NAME groupPolicyGroupName
                                ,GP.ID groupPolicyId         
	                            ,D.DEPT_NAME deptName
                                ,D.ID deptId
                                ,L.LOOKUP_NAME status
                                ,L.ID statusId
								,LL.LOOKUP_NAME accountType
								,LL.ID accountTypeId
                                ,E.[ID] empId 
                                ,E.[LOGIN_NAME] loginName 
								,E.[EMP_NO] employeeNo 
								,E.[DISPLAY_NAME] fullName 
								,E.[POSITION] position 
								,E.[EMAIL] email 
								,E.[TELEPHONE] ext 
								,E.[MOBILE_PHONE] phone 
								,E.[MANAGER] reportTo 
								,E.[GROUP_NAME] groupName 
								,E.[TEAM_ID] subDeptId 
								,E.[TEAM_NAME] subDept 
								,E.[DEPT_TYPE] devision 
								,E.[HOD] hod 
								,E.[EMP_TYPE] empType
                            FROM TICKET.AGENT A
                            INNER JOIN [HR].[VIEW_EMPLOYEE_LIST] E ON A.EMP_ID =E.ID
                            INNER JOIN TICKET.GROUP_POLICY GP ON A.GROUP_POLICY_ID = GP.ID
                            INNER JOIN TICKET.DEPARTMENT D ON A.DEPT_ID = D.ID
                            INNER JOIN TICKET.LOOKUP L ON L.LOOKUP_KEY='AGENT_STATUS' AND A.STATUS=L.LOOKUP_CODE 
							INNER JOIN TICKET.LOOKUP LL ON LL.LOOKUP_KEY='AGENT_ACCOUNT_TYPE' AND A.ACCOUNT_TYPE=LL.LOOKUP_CODE 
                            WHERE 
                            @query IS NULL OR (
	                            ISNULL(A.[DESCRIPTION],'')+' '+
	                            ISNULL(E.EMP_NO,'')+' '+
	                            ISNULL(E.DISPLAY_NAME,'')+ ' '+ 	                            
	                            ISNULL(GP.GROUP_NAME,'')+ ' '+ 
                                ISNULL(L.LOOKUP_NAME,'')+ ' '+ 
                                ISNULL(LL.LOOKUP_NAME,'')+ ' '+ 
	                            ISNULL(D.DEPT_NAME,'')
                            ) LIKE @query 
                             ORDER BY E.[DISPLAY_NAME] "
                                ;

            object queryParam = "%" + query + "%";
            if (query == null)
                queryParam = DBNull.Value;

            return SqlQuery<TicketAgentDto>(sqlString, new object[] { new SqlParameter("query", queryParam) }).ToList();
        }

        public IEnumerable<TicketTeam> getTeamByAgent(TicketAgent agent)
        {
            string sqlString = @"SELECT DISTINCT 
                                T.ID id
                                ,T.TEAM_NAME teamName
						        ,T.ALERT_ALL_MEMBERS alertAllMembers
						        ,T.ALERT_ASSIGNED_AGENT alertAssignedAgent
						        ,T.DIRECTORY_LISTING directoryListing
						        ,T.[STATUS] status						        
                                ,T.[DESCRIPTION] description
                                ,T.[CREATED_DATE] createdDate
                                ,T.[MODIFIED_DATE] modifiedDate
                                FROM TICKET.TEAM T
                                INNER JOIN TICKET.TEAM_AGENT_ASSIGN TAA ON T.ID = TAA.TEAM_ID
                                INNER JOIN TICKET.AGENT A ON TAA.AGENT_ID = A.ID
                                WHERE A.ID = @agentId "
                                ;

            return SqlQuery<TicketTeam>(sqlString, new object[] { new SqlParameter("agentId", agent.Id) }).ToList();
        }

        public Boolean isAgentExisted(TicketAgent agent)
        {
            Boolean existed = false;

            var sqlString =@"SELECT count(*) FROM TICKET.AGENT A WHERE 
                                (@agentId > 0 OR A.EMP_ID = @empId)
                                AND (@agentId <= 0 OR (A.ID != @agentId AND A.EMP_ID = @empId))";
            try
            {
                int total = DbContext.Database.SqlQuery<int>(sqlString, new object[] { new SqlParameter("@agentId", agent.Id), new SqlParameter("@empId", agent.EmpId) }).FirstOrDefault<int>();
                if (total > 0)
                {
                    existed = true;
                }
            }catch (Exception e)
            {
                Console.Write("e ", e.InnerException);
                
            }

            return existed;
        }
    }
}
