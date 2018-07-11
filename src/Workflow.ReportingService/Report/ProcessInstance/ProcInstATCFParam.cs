using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstATCFParam : ProcInstParam
    {
        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
    }

    [DataContract]
    public class ProcInstATCFSummaryParam
    {
        [DataMember(Name = "MonthFrom")]
        public int MonthFrom { get; set; }
        [DataMember(Name = "MonthTo")]
        public int MonthTo { get; set; }
        [DataMember(Name = "YearFrom")]
        public int YearFrom { get; set; }
        [DataMember(Name = "YearTo")]
        public int YearTo { get; set; }
        [DataMember(Name = "DeptNames")]
        public string DeptNames { get; set; }
        [DataMember(Name = "SqlQuery")]
        public string SqlQuery { get; set; }
    }
}