using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    //[Report(Name = "PBF Process Instance", Path = "K2Report/Reports/PROC_INST_PBF")]
    public class PBFProcInst : ProcInst
    {
        [DataMember(Name = "projectName")]
        public string PROJECT_NAME { get; set; }

        [DataMember(Name = "projectReference")]
        public string PROJECT_REFERENCE { get; set; }
    }
}
