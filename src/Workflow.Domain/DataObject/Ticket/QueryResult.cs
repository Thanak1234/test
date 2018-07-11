using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class QueryResult
    {
        public string sql { get; set; }
        public int total { get; set; }
        public List<TicketListing> tickets { get; set; }
    }
}
