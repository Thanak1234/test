using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    public class ADMCPPProcInst : ProcInst
    {
        [DataMember(Name = "model")]
        public string MODEL { get; set; }

        [DataMember(Name = "platNo")]
        public string PLATE_NO { get; set; }

        [DataMember(Name = "color")]
        public string COLOR { get; set; }

        [DataMember(Name = "yop")]
        public string YOP { get; set; }

        [DataMember(Name = "issueDate")]
        public DateTime? ISSUE_DATE { get; set; }

        [DataMember(Name = "cpsn")]
        public string CPSN { get; set; }

        [DataMember(Name = "remark")]
        public string REMARK { get; set; }
    }
}
