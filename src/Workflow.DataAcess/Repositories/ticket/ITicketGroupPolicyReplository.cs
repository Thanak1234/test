using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket
{
    public interface ITicketGroupPolicyReplository : IRepository<TicketGroupPolicy>
    {
        IEnumerable<TicketGroupPolicyDto> getGroupPolicies(string query = null);
        IEnumerable<TicketGroupPolicyTeamsDto> getTeams(int groupPolicyId = 0);
        IEnumerable<TicketGroupPolicyTeamsDto> getReportAccessTeams(int groupPolicyId = 0);
        Boolean isGroupPoliciesExisted(TicketGroupPolicyDto instance);
    }
}
