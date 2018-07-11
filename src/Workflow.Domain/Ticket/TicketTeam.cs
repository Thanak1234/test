using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketTeam : AbstractEntityLevel2
    {

        [DataMember(Name = "teamName")]
        public string TeamName { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "directoryListing")]
        public bool DirectoryListing { get; set; }
        
        [DataMember(Name = "alertAllMembers")]
        public bool AlertAllMembers { get; set; }

        [DataMember(Name = "alertAssignedAgent")]
        public bool AlertAssignedAgent { get; set; }


    }
}
