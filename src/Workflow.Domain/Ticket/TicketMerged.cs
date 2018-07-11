using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketMerged : AbstractBaseEntity
    {
        public int ActivityId { get; set; }
        public int ToActivityId { get; set; }
        public int ticketId { get; set; }
        public int ToTicketId { get; set; }

    }
}
