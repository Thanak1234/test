using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketFileAttachment : AbstractEntityLevel2
    {
        public int TicketActivityId { get; set; }
        public string file { get; set; }
    }
}
