using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketAssignment : AbstractEntityLevel2
    {
        public int TicketActivityId { get; set; }
        public int TeamId { get; set; }
        public int AssigneeId { get; set; }
        public bool Expired { get; set; } = false;
        
    }
}
