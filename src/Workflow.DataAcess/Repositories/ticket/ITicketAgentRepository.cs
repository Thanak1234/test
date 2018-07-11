using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket
{
    public interface ITicketAgentRepository : IRepository<TicketAgent>
    {   
        IEnumerable<TicketAgentDto> getAgents(string query = null);
        IEnumerable<TicketTeam> getTeamByAgent(TicketAgent agent);
        Boolean isAgentExisted(TicketAgent agent);
    }
}
