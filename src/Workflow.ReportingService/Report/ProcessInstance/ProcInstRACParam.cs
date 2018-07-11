using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstRACParam : ProcInstParam
    {
        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
        [DataMember(Name = "Item")]
        public string Item { get; set; }
        [DataMember(Name = "Reason")]
        public string Reason { get; set; }
        [DataMember(Name = "SerialNo")]
        public string SerialNo { get; set; }
        [DataMember(Name = "DIStart")]
        public DateTime? DIStart { get; set; }
        [DataMember(Name = "DIEnd")]
        public DateTime? DIEnd { get; set; }
    }
}
