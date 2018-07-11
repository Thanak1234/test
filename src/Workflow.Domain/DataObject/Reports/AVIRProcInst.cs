using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    //[Report(Name = "AVDR Process Instance", Path = "K2Report/Reports/PROC_INST_AVDR")]
    public class AVIRProcInst : ProcInst
    {
        [DataMember(Name = "location")]
        public string LOCATION { get; set; }

        [DataMember(Name = "incidentDate")]
        public DateTime INCIDENT_DATE { get; set; }

        [DataMember(Name = "complaintRegarding")]
        public string COMPLAINT_REGARDING { get; set; }

        [DataMember(Name = "complaint")]
        public string COMPLAINT { get; set; }

        [DataMember(Name = "reporterName")]
        public string REPORTER_NAME { get; set; }

        [DataMember(Name = "reporterPosition")]
        public string REPORTER_POSITION { get; set; }

        [DataMember(Name = "reporterNo")]
        public string REPORTER_NO { get; set; }
    }
}
