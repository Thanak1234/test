using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    public class ITSWDProcInst : ProcInst
    {
        [DataMember(Name = "application")]
        public string APPLICATION { get; set; }
        [DataMember(Name = "proposedChange")]
        public string PROPOSED_CHANGE { get; set; }
        [DataMember(Name = "description")]
        public string DESCRIPTION { get; set; }
        [DataMember(Name = "benefitCs")]
        public bool? BENEFIT_CS { get; set; }
        [DataMember(Name = "benefitIIS")]
        public bool? BENEFIT_IIS { get; set; }
        [DataMember(Name = "benefitRM")]
        public bool? BENEFIT_RM { get; set; }
        [DataMember(Name = "benefitOther")]
        public string BENEFIT_OTHER { get; set; }
        [DataMember(Name = "priorityConsideration")]
        public decimal? PRIORITY_CONSIDERATION { get; set; }
        [DataMember(Name = "hc")]
        public decimal? HC { get; set; }
        [DataMember(Name = "slc")]
        public decimal? SLC { get; set; }
        [DataMember(Name = "scmd")]
        public int? SCMD { get; set; }
        [DataMember(Name = "rsim")]
        public string RSIM { get; set; }
        [DataMember(Name = "rawm")]
        public string RAWM { get; set; }
        [DataMember(Name = "deliveryDate")]
        public string DELIVERY_DATE { get; set; }
        [DataMember(Name = "goLiveDate")]
        public DateTime? GO_LIVE_DATE { get; set; }
        [DataMember(Name = "devStartDate")]
        public DateTime? DEV_START_DATE { get; set; }
        [DataMember(Name = "devEndDate")]
        public DateTime? DEV_END_DATE { get; set; }
        [DataMember(Name = "devRemark")]
        public string DEV_REMARK { get; set; }
        [DataMember(Name = "qaStartDate")]
        public DateTime? QA_START_DATE { get; set; }
        [DataMember(Name = "qaEndDate")]
        public DateTime? QA_END_DATE { get; set; }
        [DataMember(Name = "qaRemark")]
        public string QA_REMARK { get; set; }
    }
}
