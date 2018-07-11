using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    public class JRAMProcInst : ProcInst
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
        [DataMember(Name = "game")]
        public string Game { get; set; }
        [DataMember(Name = "rtp")]
        public double? Rtp { get; set; }
        [DataMember(Name = "clearDate")]
        public DateTime? ClearDate { get; set; }
        [DataMember(Name = "instances")]
        public string Instances { get; set; }
        [DataMember(Name = "descr")]
        public string Descr { get; set; }
        [DataMember(Name = "props")]
        public string Props { get; set; }
        [DataMember(Name = "checkList")]
        public string CheckList { get; set; }
    }
}
