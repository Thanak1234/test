using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstAVJParam : ProcInstParam
    {
        [DataMember(Name = "ItemType")]
        public string ItemType { get; set; }

        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
    }
}
