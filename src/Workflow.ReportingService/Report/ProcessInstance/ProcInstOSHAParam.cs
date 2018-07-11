using System;
using System.Runtime.Serialization;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstOSHAParam : ProcInstParam
    {
        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
        [DataMember(Name = "NTA")] // Nature/Type of the Accident
        public string NTA { get; set; }
        [DataMember(Name = "LAI")] // Location of accident/incident
        public string LAI { get; set; }

        // Date/time of accident/incident
        [DataMember(Name = "DTAStartDate")]
        public DateTime? DTAStartDate { get; set; }
        [DataMember(Name = "DTAEndDate")]
        public DateTime? DTAEndDate { get; set; }

        [DataMember(Name = "Victim")] // Victim
        public string Victim { get; set; }
        [DataMember(Name = "Witness")] // Witness
        public string Witness { get; set; }
    }
}
