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
    public class TicketTeamAgentAssignRepository : RepositoryBase<TicketTeamAgentAssign>, ITicketTeamAgentAssignRepository
    {
        public TicketTeamAgentAssignRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<TicketTeamAgentAssign> getListByPartners(int teamId = 0, int agentId = 0)
        {
            string sql = @"SELECT  [ID] id
                              ,[TEAM_ID] teamId
                              ,[AGENT_ID] agentId
                              ,[IMMEDIATE_ASSIGN] immediateAssign
                              ,[STATUS] status
                              ,[CREATED_DATE] createdDate
                              ,[MODIFIED_DATE] modifiedDate
                              ,[DESCRIPTION] description
                          FROM [TICKET].[TEAM_AGENT_ASSIGN] T
                          WHERE 
                          T.STATUS = 'ACTIVE'
                          AND (@teamId = 0 OR T.TEAM_ID = @teamId)
                          AND (@agentId = 0 OR T.AGENT_ID = @agentId)
                          ORDER BY T.ID ";
            return SqlQuery<TicketTeamAgentAssign>(sql, new object[] { new SqlParameter("teamId", teamId) ,new SqlParameter("agentId", agentId)});
        }


    }
}
