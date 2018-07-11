using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    //[Report(Name ="Process Instance", Path = "K2Report/Reports/PROC_INST_CLASSIC")]
    public class ProcInst
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "folio")]
        public string Folio { get; set; }
        [DataMember(Name = "requestCode")]
        public string Request_Code { get; set; }
        [DataMember(Name = "application")]
        public string App_Name { get; set; }
        [DataMember(Name = "originator")]
        public string Submitted_By { get; set; }
        [DataMember(Name = "submitDate")]
        public DateTime Submitted_Date { get; set; }        [DataMember(Name = "lastActivity")]        public string Last_Activity { get; set; }
        [DataMember(Name = "lastActionDate")]
        public DateTime Last_Action_Date { get; set; }
        [DataMember(Name = "lastActionBy")]
        public string Last_Action_By { get; set; }        [DataMember(Name = "status")]        public string Status { get; set; }
        [DataMember(Name = "action")]        public string Action { get; set; }
        [DataMember(Name = "procInstId")]
        public int? Process_Instance_Id { get; set; }
        [DataMember(Name = "formUrl")]
        public string Form_Url { get; set; }
        [DataMember(Name = "workflowUrl")]
        public string Workflow_Url { get; set; }
        [DataMember(Name = "requestor")]
        public string Requestor { get; set; }
        [DataMember(Name = "noneK2")]
        public bool None_K2 { get; set; }
        [DataMember(Name = "applicationPath")]
        public string Application_Path { get; set; }
        [DataMember(Name = "activity")]
        public string CURRENT_ACTIVITY { get; set; }
    }
}
