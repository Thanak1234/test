using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketNotification : AbstractBaseEntity
    {
        public int ActivityId { get; set; }
        public string NotificationType { get; set; }
        public int EmpId { get; set; }
        public string Status { get; set; }
    }
}
