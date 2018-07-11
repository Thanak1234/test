using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketDto
    {
        [DataMember(Name = "ticketId")]
        public int TicketId { get; set; }

        [DataMember(Name = "activityCode")]
        public string ActivityCode { get; set; }
        [DataMember(Name = "actionCode")]
        public string ActionCode { get; set; }

        [DataMember(Name = "currUser")]
        public EmployeeDto CurrUser { get; set; }

        [DataMember(Name = "requestorId")]
        public int RequestorId { get; set; }
        [DataMember(Name = "ticketItemId")]
        public int TicketItemId { get; set; }
        [DataMember(Name = "subject")]
        public string Subject { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "deptOwnerId")]
        public int DeptOwnerId { get; set; }
        [DataMember(Name = "ticketTypeId")]
        public int TicketTypeId { get; set; }
        [DataMember(Name = "siteId")]
        public int SiteId { get; set; }
        [DataMember(Name = "impactId")]
        public int ImpactId { get; set; }
        [DataMember(Name = "urgencyId")]
        public int UrgencyId { get; set; }
        [DataMember(Name = "priorityId")]
        public int PriorityId { get; set; }
        [DataMember(Name = "sourceId")]
        public int SourceId { get; set; }

        [DataMember(Name = "statusId")]
        public int StatusId { get; set; }

        [DataMember(Name = "teamId")]
        public int TeamId { get; set; }
        [DataMember(Name = "assignee")]
        public int Assignee { get; set; }
        [DataMember(Name = "userAttachFiles")]
        public List<FileUploadInfo> UserAttachFiles { get; set; }
        [DataMember(Name = "userAttachFilesDel")]
        public List<FileUploadInfo> UserAttachFilesDel { get; set; }

        [DataMember(Name = "estimatedHours")]
        public decimal? EstimatedHours { get; set; }

        [DataMember(Name = "actualMinutes")]
        public decimal? ActualMinutes { get; set; }

        [DataMember(Name = "dueDate")]
        public DateTime? DueDate { get; set; }

        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        [DataMember(Name = "isAutomation")]
        public bool IsAutomation { get; set; }

        public bool IsFormIntegrated { get; set; }

        public TicketNoneReqEmpDto TicketNoneReqEmp { get; set; }

        [DataMember(Name = "slaId")]
        public int SlaId { get; set; }

        [DataMember(Name ="rootCause")]
        public string RootCause { get; set; }

        [DataMember(Name = "refType")]
        public string RefType { get; set; }

        [DataMember(Name = "reference")]
        public int? Reference { get; set; }
    }
}
