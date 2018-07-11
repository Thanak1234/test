using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    public class HGVRProcInst : ProcInst
    {
        [DataMember(Name = "RequestHeaderId")]
        public int? RequestHeaderId { get; set; }
        [DataMember(Name = "PresentedTo")]
        public string PresentedTo { get; set; }
        [DataMember(Name = "InChargedDept")]
        public string InChargedDept { get; set; }
        [DataMember(Name = "Justification")]
        public string Justification { get; set; }

        [DataMember(Name = "QuantityRequest")]
        public int? QuantityRequest { get; set; }
        [DataMember(Name = "EntitiesBearerTo")]
        public string EntitiesBearerTo { get; set; }
        [DataMember(Name = "ValidDateFrom")]
        public DateTime? ValidDateFrom { get; set; }
        [DataMember(Name = "ValidDateTo")]
        public DateTime? ValidDateTo { get; set; }
        [DataMember(Name = "TotalCashCollected")]
        public double? TotalCashCollected { get; set; }
        [DataMember(Name = "DateRequired")]
        public DateTime? DateRequired { get; set; }

        [DataMember(Name = "IssuedNoFrom")]
        public string IssuedNoFrom { get; set; }
        [DataMember(Name = "IssuedNoTo")]
        public string IssuedNoTo { get; set; }
        [DataMember(Name = "InChargedDeptFinance")]
        public string InChargedDeptFinance { get; set; }
    }
}
