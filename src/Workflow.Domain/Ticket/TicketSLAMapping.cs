using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketSLAMapping : AbstractEntityLevel2
    {
        [DataMember(Name = "slaId")]
        public int SlaId { get; set; }

        [DataMember(Name = "priorityId")]
        public int PriorityId { get; set; }

        [DataMember(Name = "typeId")]
        public int TypeId { get; set; }
    }
}
