using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    //[Report(Name ="MWO Process Instance", Path = "K2Report/Reports/PROC_INST_MWO")]
    public class MWOProcInst : ProcInst
    {
        [DataMember(Name = "Mode")]
        public string MODE { get; set; }

        [DataMember(Name = "RequestType")]
        public string REQUEST_TYPE { get; set; }

        [DataMember(Name = "ReferenceNumber")]
        public string REFERENCE_NUMBER { get; set; }

        [DataMember(Name = "Location")]
        public string LOCATION { get; set; }

        [DataMember(Name = "SubLocation")]
        public string SUB_LOCATION { get; set; }

        [DataMember(Name = "CCD")]
        public string CCD { get; set; }

        [DataMember(Name = "Department")]
        public string DEPARTMENT { get; set; }

        [DataMember(Name = "Remark")]
        public string REMARK { get; set; }

        [DataMember(Name = "WRJD")]
        public string WRJD { get; set; }

        [DataMember(Name = "TechnicianName")]
        public string TECHNICIAN_NAME { get; set; }

        [DataMember(Name = "AssignDate")]
        public DateTime? ASSIGN_DATE { get; set; }

        [DataMember(Name = "Instruction")]
        public string INSTRUCTION { get; set; }

        [DataMember(Name = "WorkType")]
        public string WORK_TYPE { get; set; }

        [DataMember(Name = "TechnicianDesc")]
        public string TC_DESC { get; set; }        
    }
}
