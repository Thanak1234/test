using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstAvirParam : ProcInstParam
    {
        [DataMember(Name = "IncidentStartDate")]
        public DateTime? IncidentStartDate { get; set; }

        [DataMember(Name = "IncidentEndDate")]
        public DateTime? IncidentEndDate { get; set; }

        [DataMember(Name = "ReporterId")]
        public int? ReporterId { get; set; }

        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
    }
}
