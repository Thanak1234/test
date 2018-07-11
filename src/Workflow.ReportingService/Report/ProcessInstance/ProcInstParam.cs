using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Core.Attributes;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstParam : Parameter
    {
        [DataMember(Name = "Folio")]
        public string RequestCode {get; set;}// string {FORM}-{000000}
        [DataMember(Name = "ParticipationType")]
        public string ParticipateType { get; set; } // requestor, originator, approver
        [DataMember(Name = "AppName")]
        public string ProcessName { get; set; }
        [DataMember(Name = "SubmittedDateStarted")]
        public DateTime? SubmitDateStart { get; set; }
        [DataMember(Name = "SubmittedDateEnded")]
        public DateTime? SubmitDateEnd { get; set; }
        [DataMember(Name = "ParticipatedDateStarted")]
        public DateTime? ParticipateDateStart { get; set; }
        [DataMember(Name = "ParticipatedDateEnded")]
        public DateTime? ParticipateDateEnd { get; set; }
        [DataMember(Name = "DeptList")]
        public string Departments { get; set; }// list of department
        [DataMember(Name = "EmployeeId")]
        public int EmployeeId { get; set; }// department
        [DataMember(Name = "Status")]
        public int Status { get; set; } //-1: All, 2: In progress, 3 Completed
        [DataMember(Name = "Action")]
        public string Action { get; set; } //Approved, Reviewed, Rejected, Reworked, Resubmitted, Done
        [DataMember(Name = "CurrentUserId")]
        public int CurrentUserId { get; set; }

        [DataMember(Name = "TotalRecord")]
        [OutputParameter(ReturnType = typeof(int))]
        public int TotalRecord { get; set; }

        [DataMember(Name = "CurrentActivity")]
        public string CurrentActivity { get; set; }
    }
}
