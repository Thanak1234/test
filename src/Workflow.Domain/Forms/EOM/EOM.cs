using System;
using System.Runtime.Serialization;

namespace Workflow.Domain.Entities.EOMRequestForm {

    [DataContract]
    public class EOMDetail {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "requestHeaderId")]
        public int RequestHeaderId { get; set; }

        [DataMember(Name = "month")]
        public DateTime Month { get; set; }

        [DataMember(Name = "aprd")]
        public double Aprd { get; set; }

        [DataMember(Name = "cfie")]
        public double Cfie { get; set; }

        [DataMember(Name = "lc")]
        public double Lc { get; set; }

        [DataMember(Name = "tmp")]
        public double Tmp { get; set; }

        [DataMember(Name = "psdm")]
        public double Psdm { get; set; }

        [DataMember(Name = "totalScore")]
        public double? TotalScore { get; set; }

        [DataMember(Name = "reason")]
        public string Reason { get; set; }

        [DataMember(Name = "cash")]
        public double? Cash { get; set; }

        [DataMember(Name = "voucher")]
        public double? Voucher { get; set; }

    }

}
