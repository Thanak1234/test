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
    public interface ITicketTeamAgentAssignRepository : IRepository<TicketTeamAgentAssign>
    {
        IEnumerable<TicketTeamAgentAssign> getListByPartners(int teamId = 0, int agentId=0);
    }
}
