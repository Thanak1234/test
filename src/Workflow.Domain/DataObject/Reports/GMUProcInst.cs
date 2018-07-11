using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    public class GMUProcInst : ProcInst
    {
        [DataMember(Name = "m")]
        public string M { get; set; }
        [DataMember(Name = "mpd")]
        public DateTime? MPD { get; set; }

        [DataMember(Name = "h")]
        public string H { get; set; }
        [DataMember(Name = "hpd")]
        public DateTime? HPD { get; set; }

        [DataMember(Name = "c")]
        public string C { get; set; }
        [DataMember(Name = "cpd")]
        public DateTime? CPD { get; set; }

        [DataMember(Name = "v")]
        public string V { get; set; }
        [DataMember(Name = "vpd")]
        public DateTime? VPD { get; set; }

        [DataMember(Name = "gmid")]
        public string Gmid { get; set; }
        [DataMember(Name = "macAddress")]
        public string MacAddress { get; set; }
        [DataMember(Name = "ip")]
        public string Ip { get; set; }
        [DataMember(Name = "remark")]
        public string Remark { get; set; }
        [DataMember(Name = "gmus")]
        public string Gmus { get; set; }
        [DataMember(Name = "descr")]
        public string Descr { get; set; }
        [DataMember(Name = "props")]
        public string Props { get; set; }
        [DataMember(Name = "checkList")]
        public string CheckList { get; set; }
    }
}
