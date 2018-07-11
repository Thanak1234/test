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
    public interface ITicketGroupPolicyReportAssignRepository : IRepository<TicketGroupPolicyReportAssign>
    {
        IEnumerable<TicketGroupPolicyTeamsDto> getListByPartners(int teamId = 0, int groupPolicyId=0);
    }
}
