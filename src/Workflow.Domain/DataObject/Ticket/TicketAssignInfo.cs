using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketAssignInfo
    {
        public int id { get; set; }
        public string team { get; set; }
        public string assignee { get; set; }
        public string empNoAssignee { get; set; }
        public bool expired { get; set; }
    }
}
