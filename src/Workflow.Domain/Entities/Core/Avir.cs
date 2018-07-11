

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Workflow.Domain.Entities.Core
{
    [DataContract]
    public class Avir {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "requestHeaderId")]
        public int RequestHeaderId { get; set; }

        [DataMember(Name = "receiverId")]
        public int ReceiverId { get; set; }

        [DataMember(Name = "location")]
        public string Location { get; set; }

        [DataMember(Name = "incidentDate")]
        public DateTime IncidentDate { get; set; }

        [DataMember(Name = "reporterId")]
        public int ReporterId { get; set; }

        [DataMember(Name = "complaintRegarding")]
        public string ComplaintRegarding { get; set; }

        [DataMember(Name = "complaint")]
        public string Complaint { get; set; }
    }
}
