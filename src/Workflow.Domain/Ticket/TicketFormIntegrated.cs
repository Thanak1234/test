using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketFormIntegrated : AbstractBaseEntity
    {
        public int RequestHaderId { get; set; }
        public int ActivityId { get; set; }
        public int TicketDeptId { get; set; }
        public int TicketId { get; set; }
        public string Status { get; set; }
    }
}
