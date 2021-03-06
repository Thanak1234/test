﻿using System;
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
    public class TicketTeamRepository : RepositoryBase<TicketTeam>, ITicketTeamRepository
    {
        public TicketTeamRepository(IDbFactory dbFactory) : base(dbFactory) { }


        public IEnumerable<TicketTeamDto> getTeams(string query = null)
        {
            string sqlString = @"SELECT
                        T.ID id
                        ,T.TEAM_NAME teamName
						,T.ALERT_ALL_MEMBERS alertAllMembers
						,T.ALERT_ASSIGNED_AGENT alertAssignedAgent
						,T.DIRECTORY_LISTING directoryListing
						,T.STATUS status
						,L.ID statusId
                        ,T.DESCRIPTION description
                        ,T.[CREATED_DATE] createdDate
                        ,T.[MODIFIED_DATE] modifiedDate
                        FROM TICKET.TEAM T
						INNER JOIN TICKET.LOOKUP L ON L.ACTIVE = 1 AND T.STATUS = L.LOOKUP_CODE AND L.LOOKUP_KEY = 'TEAM_STATUS'
                        WHERE
                        @query IS NULL OR (ISNULL(T.TEAM_NAME, '')+ ' '+ ISNULL(T.[DESCRIPTION],'')+ ' '+ ISNULL(L.LOOKUP_NAME,'')) LIKE @query
                        ORDER BY T.TEAM_NAME "
                               ;

            object queryParam = "%" + query + "%";
            if (query == null)
                queryParam = DBNull.Value;

            return SqlQuery<TicketTeamDto>(sqlString, new object[] { new SqlParameter("query", queryParam) }).ToList();
        }

        public IEnumerable<TicketAgentTeamsDto> getAgentTeams(int agentId = 0)
        {
            string sqlString = @"SELECT
                                T.ID id
                                ,T.TEAM_NAME teamName
                                ,T.ALERT_ALL_MEMBERS alertAllMembers
                                ,T.ALERT_ASSIGNED_AGENT alertAssignedAgent
                                ,T.DIRECTORY_LISTING directoryListing
                                ,T.STATUS status
                                ,L.ID statusId
                                ,T.DESCRIPTION description
                                ,T.[CREATED_DATE] createdDate
                                ,T.[MODIFIED_DATE] modifiedDate
                                ,ISNULL(A.ID,0) agentId
                                ,CASE WHEN A.ID >0 THEN CAST(ISNULL(TAA.IMMEDIATE_ASSIGN,0) AS BIT) ELSE CAST(0 AS BIT) END immediateAssign
                                FROM TICKET.TEAM T
                                INNER JOIN TICKET.LOOKUP L ON L.ACTIVE = 1 AND T.STATUS = L.LOOKUP_CODE AND L.LOOKUP_KEY = 'TEAM_STATUS'
                                LEFT JOIN TICKET.TEAM_AGENT_ASSIGN TAA ON T.ID = TAA.TEAM_ID
                                LEFT JOIN TICKET.AGENT A ON TAA.AGENT_ID = A.ID AND A.ID = @agentId                                
                                ORDER BY T.TEAM_NAME "
                               ;

            return SqlQuery<TicketAgentTeamsDto>(sqlString, new object[] { new SqlParameter("agentId", agentId)}).ToList();
        }

        public IEnumerable<TicketTeamAgentsDto> getTeamAgents(int teamId = 0)
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
	                            ,ISNULL(T.ID,0) teamId
	                            ,ISNULL(TAA.IMMEDIATE_ASSIGN, CAST(0 AS BIT)) immediateAssign
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
                            INNER JOIN TICKET.TEAM_AGENT_ASSIGN TAA ON A.ID = TAA.AGENT_ID 
                            INNER JOIN TICKET.TEAM T ON TAA.TEAM_ID = T.ID
                            WHERE                             
                            TAA.STATUS != 'DELETE'
                            AND (@teamId > 0 AND TAA.TEAM_ID = @teamId)
                            ORDER BY E.DISPLAY_NAME "
                               ;

            return SqlQuery<TicketTeamAgentsDto>(sqlString, new object[] { new SqlParameter("teamId", teamId) }).ToList();
        }

        public List<int> GetTeamsByEmployeeId(int empId) {
            List<int> teams = null;
            string sqlString = @"SELECT TM.ID FROM TICKET.TEAM TM
                                    INNER JOIN TICKET.TEAM_AGENT_ASSIGN AT ON TM.ID = AT.TEAM_ID
                                    INNER JOIN TICKET.AGENT A ON A.ID = AT.AGENT_ID
									WHERE A.EMP_ID = @EmpId AND A.STATUS = 'ACTIVE' AND TM.STATUS = 'ACTIVE' AND AT.STATUS = 'ACTIVE'";
            try {
                teams = DbContext.Database.SqlQuery<int>(sqlString, new object[] { new SqlParameter("@EmpId", empId)}).ToList();                
            } catch (Exception e) {
                Console.Write("e ", e.InnerException);
            }

            return teams;
        }

        public Boolean isTeamExisted(TicketTeamDto instance)
        {
            Boolean existed = false;
            var sqlString = @"SELECT count(*) FROM TICKET.TEAM A 
                WHERE 
                (@teamId > 0 OR RTRIM(LTRIM(LOWER(A.TEAM_NAME))) = RTRIM(LTRIM(LOWER(@teamName))))
                AND (@teamId <= 0 OR (A.ID != @teamId AND RTRIM(LTRIM(LOWER(A.TEAM_NAME))) = RTRIM(LTRIM(LOWER(@teamName)))))";
            try
            {
                int total = DbContext.Database.SqlQuery<int>(sqlString, new object[] { new SqlParameter("@teamId", instance.id), new SqlParameter("@teamName", instance.teamName) }).FirstOrDefault<int>();
                if (total > 0)
                {
                    existed = true;
                }
            }
            catch (Exception e)
            {
                Console.Write("e ", e.InnerException);
            }
            return existed;
        }

        public IEnumerable<TicketTeamDto> getTeams(TicketSettingCriteria criteria)
        {
            string sqlString = @"SELECT
                        T.ID id
                        ,T.TEAM_NAME teamName
						,T.ALERT_ALL_MEMBERS alertAllMembers
						,T.ALERT_ASSIGNED_AGENT alertAssignedAgent
						,T.DIRECTORY_LISTING directoryListing
						,T.STATUS status
						,L.ID statusId
                        ,T.DESCRIPTION description
                        ,T.[CREATED_DATE] createdDate
                        ,T.[MODIFIED_DATE] modifiedDate
                        FROM TICKET.TEAM T
						INNER JOIN TICKET.LOOKUP L ON L.ACTIVE = 1 AND T.STATUS = L.LOOKUP_CODE AND L.LOOKUP_KEY = 'TEAM_STATUS'
                        WHERE
                        (@query IS NULL OR (ISNULL(T.TEAM_NAME, '')+ ' '+ ISNULL(T.[DESCRIPTION],'')+ ' '+ ISNULL(L.LOOKUP_NAME,'')) LIKE @query)
                        AND (@status IS NULL OR @status = 'ALL' OR LOWER(T.STATUS) = LOWER(@status))
                        ORDER BY T.TEAM_NAME "
                               ;

            object queryParam = "%" + criteria.query + "%";
            if (criteria.query == null)
                queryParam = DBNull.Value;

            object statusParam = criteria.status;
            if (criteria.status == null)
                statusParam = DBNull.Value;

            return SqlQuery<TicketTeamDto>(sqlString, new object[] { new SqlParameter("query", queryParam), new SqlParameter("status", statusParam) }).ToList();
        }

    }
}
