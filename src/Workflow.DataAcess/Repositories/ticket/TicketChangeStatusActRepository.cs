using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket
{
    public class TicketChangeStatusActRepository : RepositoryBase<TicketChangeActivity> , ITicketChangeStatusActRepository
    {
        public TicketChangeStatusActRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
