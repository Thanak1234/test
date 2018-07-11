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
    public class TicketGroupPolicyReportAssignRepository : RepositoryBase<TicketGroupPolicyReportAssign>, ITicketGroupPolicyReportAssignRepository
    {
        public TicketGroupPolicyReportAssignRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<TicketGroupPolicyTeamsDto> getListByPartners(int teamId = 0, int groupPolicyId = 0)
        {
            string sql = @"SELECT  t.ID id
                              ,t.TEAM_ID teamId
                              ,t.GROUP_ACCESS_ID groupPolicyId                     
                              ,[CREATED_DATE] createdDate
                          FROM [TICKET].TEAM_REPORT_ACCESS T
                          WHERE 
                          (@teamId = 0 OR T.TEAM_ID = @teamId)
                          AND (@groupPolicyId = 0 OR T.GROUP_ACCESS_ID = @groupPolicyId)
                          ORDER BY T.ID ";
            return SqlQuery<TicketGroupPolicyTeamsDto>(sql, new object[] { new SqlParameter("teamId", teamId) ,new SqlParameter("groupPolicyId", groupPolicyId) });
        }


    }
}
