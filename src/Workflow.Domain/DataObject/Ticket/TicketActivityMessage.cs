using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketActivityMessage
    {
        public string TicketNo { get; set; }
        public string Message { get; set; }
        public List<SimpleActivityViewDto> activities { get; set; }
    }
}
