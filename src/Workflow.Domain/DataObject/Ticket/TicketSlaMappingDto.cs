using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketSlaMappingDto
    {
        [DataMember(Name = "id")]
        public int id { get; set; }
        
        [DataMember(Name = "ticketType")]
        public string ticketType { get; set; }
        
        [DataMember(Name = "priority")]
        public string priority { get; set; }

        [DataMember(Name = "sla")]
        public string sla { get; set; }

        [DataMember(Name = "typeId")]
        public int typeId { get; set; }

        [DataMember(Name = "priorityId")]
        public int priorityId { get; set; }

        [DataMember(Name = "slaId")]
        public int slaId { get; set; }

        [DataMember(Name = "description")]
        public string description { get; set; }
    }
}
