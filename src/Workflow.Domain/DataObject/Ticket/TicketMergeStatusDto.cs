using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketMergeStatusDto
    {
        [DataMember(Name = "action")]
        public string Action { get; set; }
        [DataMember(Name = "ticketNo")]
        public string TicketNo { get; set; }
        [DataMember(Name = "ticketId")]
        public int TicketId { get; set; }
        [DataMember(Name = "subject")]
        public string Subject { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}
