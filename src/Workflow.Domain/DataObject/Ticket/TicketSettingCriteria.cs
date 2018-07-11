using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketSettingCriteria
    {
        [DataMember(Name = "query")]
        public string query { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }
    }
}
