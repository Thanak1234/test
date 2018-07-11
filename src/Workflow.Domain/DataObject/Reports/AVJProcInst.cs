using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    //[Report(Name ="AVJ Process Instance", Path = "K2Report/Reports/PROC_INST_AVJ")]
    public class AVJProcInst : ProcInst
    {
        [DataMember(Name = "itemTypeName")]
        public string ITEM_TYPE_NAME { get; set;}
    }
}
