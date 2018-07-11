using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketPriorityDto
    {
        [DataMember(Name = "id")]
        public int id { get; set; }
        
        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime createdDate { get; set; }

        [DataMember(Name = "modifiedDate")]
        public DateTime modifiedDate { get; set; }
        
        public string priorityName { get; set; }  
        
        public string slaName { get; set; }

        public int slaId { get; set; }

        public string slaDescription { get; set; }
    }
}
