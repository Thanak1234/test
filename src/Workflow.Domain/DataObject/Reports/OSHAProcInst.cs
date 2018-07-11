using System;
using System.Runtime.Serialization;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    public class OSHAProcInst : ProcInst
    {
        [DataMember(Name = "nta")] // Nature/Type of the Accident
        public string NTA { get; set; } 
        [DataMember(Name = "lai")] // Location of accident/incident
        public string LAI { get; set; }
        [DataMember(Name = "dta")] // Date/Time of accident/incident
        public DateTime? DTA { get; set; }
        [DataMember(Name = "ca")] // Cause of accident/incident
        public string CA { get; set; }
        [DataMember(Name = "cmtSupervisor")] // HOD/Supervisor's Comments
        public string HSC { get; set; }
        [DataMember(Name = "cmtOSHA")] // OSHA - Comments
        public string ACNR { get; set; }
        [DataMember(Name = "actReqHOD")] // Action Taken by Requestor's HOD
        public string AT { get; set; }
    }
}
