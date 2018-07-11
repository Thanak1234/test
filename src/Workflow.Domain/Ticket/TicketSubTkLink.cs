using System;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketSubTkLink : AbstractBaseEntity
    {
        public int ActivityId { get; set; }
        public int SubTicketActId { get; set; }
    }
}
