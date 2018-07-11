using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketGroupPolicyTeamsDto
    {
        [DataMember(Name = "id")]
        public int id { get; set; }

        [DataMember(Name = "teamName")]
        public string teamName { get; set; }
        

        [DataMember(Name = "directoryListing")]
        public bool directoryListing { get; set; }
        

        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime createdDate { get; set; }

        [DataMember(Name = "modifiedDate")]
        public DateTime modifiedDate { get; set; }

        public int groupPolicyId { get; set; }
    

    }
}
