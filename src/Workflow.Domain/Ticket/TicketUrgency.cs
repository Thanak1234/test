using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketUrgency : AbstractEntityLevel2
    {
        [DataMember(Name = "urgencyName")]
        public string UrgencyName { get; set; }    
    }
}
