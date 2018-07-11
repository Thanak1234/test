using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketNoneReqEmpDto
    {
        public int EmailItemId { get; set; }
        public string Originator { get; set; }
        public string Receipient { get; set; }
        public string Cc { get; set; }
    }
}
