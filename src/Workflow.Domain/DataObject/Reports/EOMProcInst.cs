using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    //[Report(Name ="EOM Process Instance", Path = "K2Report/Reports/PROC_INST_EOM")]
    public class EOMProcInst : ProcInst
    {
        [DataMember(Name = "month")]
        public DateTime MONTH { get; set; }

        [DataMember(Name = "tpk")]
        public double TPK { get; set; }

        [DataMember(Name = "pi")]
        public double PI { get; set; }

        [DataMember(Name = "aprd")]
        public double APRD { get; set; }

        [DataMember(Name = "cfie")]
        public double CFIE { get; set; }

        [DataMember(Name = "rbtw")]
        public double RBTW { get; set; }

        [DataMember(Name = "ec")]
        public double EC { get; set; }

        [DataMember(Name = "leo")]
        public double LEO { get; set; }

        [DataMember(Name = "ci")]
        public double CI { get; set; }

        [DataMember(Name = "lc")]
        public double LC { get; set; }

        [DataMember(Name = "tmp")]
        public double TMP { get; set; }

        [DataMember(Name = "psdm")]
        public double PSD { get; set; }

        [DataMember(Name = "cm")]
        public double CMD { get; set; }

        [DataMember(Name = "total")]
        public double? TOTAL_SCORE { get; set; }

        [DataMember(Name = "reason")]
        public string REASON { get; set; }

        [DataMember(Name = "fullDeptName")]
        public string FULL_DEPT_NAME { get; set; }

        [DataMember(Name = "empNo")]
        public string EMP_NO { get; set; }

        [DataMember(Name = "position")]
        public string POSITION { get; set; }
    }
}
