using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstCCRParam : ProcInstParam
    {
        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "RegisterNo")]
        public string RegisterNo { get; set; }
        [DataMember(Name = "ContactName")]
        public string ContactName { get; set; }
        [DataMember(Name = "IssueBy")]
        public string IssueBy { get; set; }
        [DataMember(Name = "Bcj")]
        public string Bcj { get; set; }
        [DataMember(Name = "AtSa")]
        public bool? AtSa { get; set; }
        [DataMember(Name = "AtSpa")]
        public bool? AtSpa { get; set; }
        [DataMember(Name = "AtLa")]
        public bool? AtLa { get; set; }
        [DataMember(Name = "AtCa")]
        public bool? AtCa { get; set; }
        [DataMember(Name = "AtLea")]
        public bool? AtLea { get; set; }
        [DataMember(Name = "AtEa")]
        public bool? AtEa { get; set; }
        [DataMember(Name = "StatusNew")]
        public bool? StatusNew { get; set; }
        [DataMember(Name = "StatusRenewal")]
        public bool? StatusRenewal { get; set; }
        [DataMember(Name = "StatusReplacement")]
        public bool? StatusReplacement { get; set; }
        [DataMember(Name = "StatusAddendum")]
        public bool? StatusAddendum { get; set; }
        [DataMember(Name = "AtOther")]
        public string AtOther { get; set; }
    }
}
