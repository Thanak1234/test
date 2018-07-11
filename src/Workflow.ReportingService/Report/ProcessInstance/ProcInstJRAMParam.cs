using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstJRAMParam : ProcInstParam
    {
        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
        [DataMember(Name = "Property")]
        public string Property { get; set; }
        [DataMember(Name = "Gmid")]
        public string GMID { get; set; }
    }
}
