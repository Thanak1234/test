using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstEOMParam : ProcInstParam
    {
        [DataMember(Name = "Activity")]
        public string Activity { get; set; }

        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }

        [DataMember(Name = "StartMonth")]
        public DateTime? StartMonth { get; set; }

        [DataMember(Name = "EndMonth")]
        public DateTime? EndMonth { get; set; }
    }
}
