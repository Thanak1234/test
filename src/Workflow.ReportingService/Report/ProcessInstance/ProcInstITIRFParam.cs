using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstITIRFParam : ProcInstParam
    {
        [DataMember(Name = "VendorName")]
        public string VenderName { get; set; }

        [DataMember(Name = "SendDateFrom")]
        public DateTime? SendDateFrom { get; set; }

        [DataMember(Name = "SendDateTo")]
        public DateTime? SendDateTo { get; set; }

        [DataMember(Name = "ReturnDateFrom")]
        public DateTime? ReturnDateFrom { get; set; }

        [DataMember(Name = "ReturnDateTo")]
        public DateTime? ReturnDateTo { get; set; }

        [DataMember(Name = "IsReturn")]
        public bool IsReturn { get; set; } = true;

        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
    }
}
