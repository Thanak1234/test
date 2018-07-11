using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstITParam : ProcInstParam
    {
        [DataMember(Name = "ITItem")]
        public int? ITItem { get; set; }
        [DataMember(Name = "ITItemType")]
        public int? ITItemType { get; set; }
        [DataMember(Name = "ITRoleId")]
        public int? ITRoleId { get; set; }
        [DataMember(Name = "ITSectionId")]
        public int? ITSectionId { get; set; }
    }
}
