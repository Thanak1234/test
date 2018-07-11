using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Scheduler {

    public class FormLongPending {

        public int? Id { get; set; }

        public string Folio { get; set; }
             
        public string RequestCode { get; set; }
             
        public string AppName { get; set; }
             
        public string SubmittedBy { get; set; }
             
        public DateTime SubmittedDate { get; set; }
             
        public string LastActivity { get; set; }
             
        public DateTime LastActionDate { get; set; }
             
        public string LastActionBy { get; set; }
             
        public string Status { get; set; }
             
        public string Action { get; set; }
             
        public int? ProcessInstanceId { get; set; }
             
        public string FormUrl { get; set; }
             
        public string WorkflowUrl { get; set; }
             
        public string Requestor { get; set; }
             
        public string RequestorNo { get; set; }
             
        public string DeptName { get; set; }
             
        public bool? NoneK2 { get; set; }
             
        public string ApplicationPath { get; set; }
             
        public int? Days { get; set; }
    }
}
