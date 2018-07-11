using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstMTParam : ProcInstParam
    {
        [DataMember(Name = "Shift")]
        public string Shift { get; set; }

        [DataMember(Name = "FitToWork")]
        public int FitToWork { get; set; }

        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
    }
}
