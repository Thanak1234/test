using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketActivity : AbstractEntityLevel2
    {
        public int TicketId { get; set; }
        public string ActivityType { get; set; }
        public string Action { get; set; }
        public int ActionBy { get; set; }
        public Ticket Ticket { get; set; }

    }
}
