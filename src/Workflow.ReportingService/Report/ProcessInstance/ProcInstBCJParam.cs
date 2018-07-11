using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstBCJParam : ProcInstParam
    {
        [DataMember(Name = "CapexCateId")]
        public int? CapexCateId { get; set; }

        [DataMember(Name = "CoBranchName")]
        public string CoBranchName { get; set; }

        [DataMember(Name = "ProjectName")]
        public string ProjectName { get; set; }

        [DataMember(Name = "EstinmateCapex")]
        public double? EstinmateCapex { get; set; }

        [DataMember(Name = "Operator")]
        public int Operator { get; set; }

        [DataMember(Name = "PONumber")]
        public string PONumber { get; set; }

        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
    }
}