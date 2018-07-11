using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstAvrdParam : ProcInstParam
    {
        [DataMember(Name = "IncidentStartDate")]
        public DateTime? IncidentStartDate { get; set; }

        [DataMember(Name = "IncidentEndDate")]
        public DateTime? IncidentEndDate { get; set; }

        [DataMember(Name = "SDL")]
        public string SDL { get; set; }

        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
    }
}
