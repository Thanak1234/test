using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketPriority : AbstractEntityLevel2
    {

        [DataMember(Name = "priorityName")]
        public string PriorityName { get; set; }

        [DataMember(Name ="slaId")]
        public int SlaId { get; set; }
        
    }
}
