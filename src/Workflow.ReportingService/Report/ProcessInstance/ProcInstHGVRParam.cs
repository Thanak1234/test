using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstHGVRParam : ProcInstParam
    {
        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
        [DataMember(Name = "InChargedDept")]
        public string InChargedDept { get; set; }
        [DataMember(Name = "QuantityRequest")]
        public int QuantityRequest { get; set; }
    }
}
