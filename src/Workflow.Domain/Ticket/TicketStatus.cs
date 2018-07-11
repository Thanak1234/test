using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketStatus : AbstractEntityLevel2
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "stateId")]
        public int StateId { get; set; }
        [DataMember(Name = "classify")]
        public string Classify{ get; set; }
    }
}
