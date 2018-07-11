using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketTeamAgentAssign   : AbstractEntityLevel2
    {

        [DataMember(Name = "teamId")]
        public int TeamId { get; set; }

        [DataMember(Name = "agentId")]
        public int AgentId { get; set; }

        [DataMember(Name = "immediateAssign")]
        public bool ImmediateAssign { get; set; }
        
        [DataMember(Name = "status")]
        public string Status { get; set; }
        

    }
}
