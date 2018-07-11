using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketSLA : AbstractEntityLevel2
    {
        [DataMember(Name = "slaName")]
        public string SlaName { get; set; }

        [DataMember(Name = "gracePeriod")]
        public int GracePeriod { get; set; }

        [DataMember(Name = "firstResponsePeriod")]
        public int FirstResponsePeriod { get; set; }
        
        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}
