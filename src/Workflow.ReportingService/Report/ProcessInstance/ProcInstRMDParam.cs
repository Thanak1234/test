using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstRMDParam : ProcInstParam
    {
        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
    }
}