using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstPBFParam : ProcInstParam
    {
        [DataMember(Name = "ItemId")]
        public int ItemId { get; set; }

        [DataMember(Name = "ProjectName")]
        public string ProjectName { get; set; }

        [DataMember(Name = "DatelineStarted")]
        public DateTime? DatelineStarted { get; set; }

        [DataMember(Name = "DatelineEnded")]
        public DateTime? DatelineEnded { get; set; }

        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
    }
}