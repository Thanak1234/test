using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    public class IAVAFProcInst : ProcInst
    {
        [DataMember(Name = "adjType")]
        public string ADJ_TYPE { get; set; }
        [DataMember(Name = "remark")]
        public string REMARK { get; set; }
        [DataMember(Name = "gamingDate")]
        public DateTime? GAMING_DATE { get; set; }
        [DataMember(Name = "mcidLocn")]
        public string MCID_LOCN { get; set; }
        [DataMember(Name = "varianceType")]
        public string VARIANCE_TYPE { get; set; }
        [DataMember(Name = "subject")]
        public string SUBJECT { get; set; }
        [DataMember(Name = "incidentRptRef")]
        public string INCIDENT_RPT_REF { get; set; }
        [DataMember(Name = "instanceId")]
        public long? INSTANCE_ID { get; set; }
        [DataMember(Name = "area")]
        public string AREA { get; set; }
        [DataMember(Name = "rptComparison")]
        public string RPT_COMPARISON { get; set; }
        [DataMember(Name = "amount")]
        public Decimal? AMOUNT { get; set; }
        [DataMember(Name = "comment")]
        public string COMMENT { get; set; }
    }
}
