

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Workflow.Domain.Entities.Core
{
    [DataContract]
    public class Avdr {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "reporterId")]
        public int ReporterId { get; set; }

        [DataMember(Name = "requestHeaderId")]
        public int RequestHeaderId { get; set; }

        [DataMember(Name = "incidentDate")]
        public DateTime IncidentDate { get; set; }

        [DataMember(Name = "sdl")]
        public string SDL { get; set; }

        [DataMember(Name = "elocation")]
        public string ELocation { get; set; }

        [DataMember(Name = "dle")]
        public string DLE { get; set; }

        [DataMember(Name = "ein")]
        public string EIN { get; set; }

        [DataMember(Name = "hedl")]
        public string HEDL { get; set; }

        [DataMember(Name = "at")]
        public string AT { get; set; }

        [DataMember(Name = "ecrr")]
        public string ECRR { get; set; }

        [DataMember(Name = "dcirs")]
        public string DCIRS { get; set; }
    }
}
