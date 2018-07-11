using System;
using System.Runtime.Serialization;

namespace Workflow.Domain.Entities.ADMCPPForm {

    [DataContract]
    public class ADMCPP {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "requestHeaderId")]
        public int RequestHeaderId { get; set; }

        [DataMember(Name = "model")]
        public string Model { get; set; }

        [DataMember(Name = "plateNo")]
        public string PlateNo { get; set; }

        [DataMember(Name = "color")]
        public string Color { get; set; }

        [DataMember(Name = "yop")]
        public string YOP { get; set; }

        [DataMember(Name = "cpsn")]
        public string CPSN { get; set; }

        [DataMember(Name = "issueDate")]
        public DateTime? IssueDate { get; set; }

        [DataMember(Name = "remark")]
        public string Remark { get; set; }

    }

}
