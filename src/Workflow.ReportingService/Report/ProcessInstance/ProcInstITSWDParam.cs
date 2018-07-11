using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstITSWDParam : ProcInstParam
    {
        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
        [DataMember(Name = "Application")]
        public string Application { get; set; }
        [DataMember(Name = "PurposedChange")]
        public string PurposedChange { get; set; }
        [DataMember(Name = "TD")]
        public string TD { get; set; }
        [DataMember(Name = "GLStartDate")]
        public DateTime? GLStartDate { get; set; }
        [DataMember(Name = "GLEndDate")]
        public DateTime? GLEndDate { get; set; }
    }
}
