using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket
{
    public class TicketAssignmentRepository : RepositoryBase<TicketAssignment>, ITicketAssignmentRepository
    {
        public TicketAssignmentRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
