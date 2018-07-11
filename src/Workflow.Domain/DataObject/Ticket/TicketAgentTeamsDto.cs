using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketAgentTeamsDto
    {
        [DataMember(Name = "id")]
        public int id { get; set; }

        [DataMember(Name = "teamName")]
        public string teamName { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "statusId")]
        public int statusId { get; set; }

        [DataMember(Name = "alertAllMembers")]
        public bool alertAllMembers { get; set; }

        [DataMember(Name = "alertAssignedAgent")]
        public bool alertAssignedAgent { get; set; }

        [DataMember(Name = "directoryListing")]
        public bool directoryListing { get; set; }
        
        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime createdDate { get; set; }

        [DataMember(Name = "modifiedDate")]
        public DateTime modifiedDate { get; set; }

        [DataMember(Name ="agentId")]
        public int agentId { get; set; }

        [DataMember(Name = "immediateAssign")]
        public bool immediateAssign { get; set; }

    }
}
