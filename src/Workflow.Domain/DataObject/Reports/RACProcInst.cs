using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    public class RACProcInst : ProcInst
    {
        [DataMember(Name = "item")]
        public string ITEM { get; set; }
        [DataMember(Name = "reason")]
        public string REASON { get; set; }
        [DataMember(Name = "remark")]
        public string REMARK { get; set; }
        [DataMember(Name = "dateissue")]
        public DateTime? DATE_ISSUE { get; set; }
        [DataMember(Name = "serialno")]
        public string SERIAL_NO { get; set; }
    }
}
