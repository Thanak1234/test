using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    public class ADMSRProcInst : ProcInst
    {
        [DataMember(Name = "dr")]
        public string DR { get; set; }

        [DataMember(Name = "dsrj")]
        public string DSRJ { get; set; }

        [DataMember(Name = "adc")]
        public string ADC { get; set; }

        [DataMember(Name = "salod")]
        public bool? SALOD { get; set; }

        [DataMember(Name = "efinance")]
        public bool? E_FINANCE { get; set; }

        [DataMember(Name = "ecc")]
        public bool? E_CC { get; set; }

        [DataMember(Name = "epurchasing")]
        public bool? E_PURCHASING { get; set; }

        [DataMember(Name = "slod")]
        public bool? SLOD { get; set; }
    }
}
