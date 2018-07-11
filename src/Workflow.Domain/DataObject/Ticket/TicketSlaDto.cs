using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketSlaDto
    {
        [DataMember(Name = "id")]
        public int id { get; set; }
        
        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime createdDate { get; set; }

        [DataMember(Name = "modifiedDate")]
        public DateTime modifiedDate { get; set; }

        [DataMember(Name = "slaName")]
        public string slaName { get; set; }

        [DataMember(Name = "gracePeriod")]
        public int gracePeriod { get; set; }

        [DataMember(Name = "firstResponsePeriod")]
        public int firstResponsePeriod { get; set; }

        public string statusName { get; set; }
        public string status { get; set; }

    }
}
