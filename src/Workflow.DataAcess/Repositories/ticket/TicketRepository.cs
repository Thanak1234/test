using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket
{
    public class TicketRepository : RepositoryBase<Ticket> , ITicketRepository
    {
        public TicketRepository(IDbFactory dbFactory) : base(dbFactory){ }

        public string getLastTicket()
        {
            var lastTicketNo =  DbSet.Max(t=>t.TicketNo);
            return lastTicketNo;
        }
    }
}
