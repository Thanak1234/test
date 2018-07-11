using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstADMCPPParam : ProcInstParam
    {
        [DataMember(Name = "Model")]
        public string Model { get; set; }

        [DataMember(Name = "PlatNo")]
        public string PlatNo { get; set; }

        [DataMember(Name = "CPSN")]
        public string CPSN { get; set; }

        [DataMember(Name = "IssueDate")]
        public DateTime? IssueDate { get; set; }

        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
    }
}
