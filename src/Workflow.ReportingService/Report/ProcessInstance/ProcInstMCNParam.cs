using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstMCNParam : ProcInstParam
    {
        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }

        [DataMember(Name = "Type")]
        public string Type { get; set; }

        [DataMember(Name = "MCID")]
        public int MCID { get; set; }
    }
}
