using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketNoneReqEmp : AbstractBaseEntity
    {
        public int TicketId { get; set; }
        public int EmailItemId { get; set; }
        public string Originator { get; set; }
        public string Receipient { get; set; }
        public string Cc { get; set; }
    }
}
