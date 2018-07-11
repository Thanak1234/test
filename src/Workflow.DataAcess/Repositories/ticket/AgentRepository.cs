using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket
{
    public class AgentRepository : RepositoryBase<TicketAgent>, IAgentRepository
    {
        public AgentRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
