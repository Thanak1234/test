using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketGroupPolicy: AbstractEntityLevel2
    {

        public const string OWN_TICKET = "OWN_TICKET", TEAM_TICKET = "TEAM_TICKET", DEPT_TICKET = "TEAM_TICKET";

        [DataMember(Name="groupName")]
        public string GroupName { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "createTicket")]
        public bool CreateTicket { get; set; }

        [DataMember(Name = "editTicket")]
        public bool EditTicket { get; set; }

        [DataMember(Name = "editRequestor")]
        public bool EditRequestor { get; set; }

        [DataMember(Name = "postTicket")]
        public bool PostTicket { get; set; }

        [DataMember(Name = "closeTicket")]
        public bool CloseTicket { get; set; }

        [DataMember(Name = "assignTicket")]
        public bool AssignTicket { get; set; }

        [DataMember(Name = "mergeTicket")]
        public bool MergeTicket { get; set; }

        [DataMember(Name = "deleteTicket")]
        public bool DeleteTicket { get; set; }

        [DataMember(Name = "deptTransfer")]
        public bool DeptTransfer { get; set; }

        [DataMember(Name = "changeStatus")]
        public bool ChangeStatus { get; set; }

        [DataMember(Name = "limitAccess")]
        public string LimitAccess { get; set; }

        [DataMember(Name = "newTicketNotify")]
        public string NewTicketNotify { get; set; }

        [DataMember(Name = "assignedNotify")]
        public string AssignedNotify { get; set; }

        [DataMember(Name = "replyNotify")]
        public string ReplyNotify { get; set; }

        [DataMember(Name = "changeStatusNotify")]
        public string ChangeStatusNotify { get; set; }

        [DataMember(Name = "subTicket")]
        public bool SubTicket { get; set; }

        [DataMember(Name = "reportAccess")]
        public string ReportAccess { get; set; }
    }
}
