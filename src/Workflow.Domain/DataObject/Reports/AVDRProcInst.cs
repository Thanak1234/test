using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    //[Report(Name = "AVDR Process Instance", Path = "K2Report/Reports/PROC_INST_AVDR")]
    public class AVDRProcInst : ProcInst
    {
        [DataMember(Name = "incidentDate")]
        public DateTime INCIDENT_DATE { get; set; }

        [DataMember(Name = "sdl")]
        public string SDL { get; set; }

        [DataMember(Name = "elocation")]
        public string ELOCATION { get; set; }

        [DataMember(Name = "dle")]
        public string DLE { get; set; }

        [DataMember(Name = "ein")]
        public string EIN { get; set; }

        [DataMember(Name = "hedl")]
        public string HEDL { get; set; }

        [DataMember(Name = "at")]
        public string AT { get; set; }

        [DataMember(Name = "ecrr")]
        public string ECRR { get; set; }

        [DataMember(Name = "dcirs")]
        public string DCIRS { get; set; }
    }
}
