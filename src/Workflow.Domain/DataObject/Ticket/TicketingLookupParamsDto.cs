using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketingLookupParamsDto {        
        public string key { get; set; }
        public bool includeRemoved { get; set; }
    }
}
