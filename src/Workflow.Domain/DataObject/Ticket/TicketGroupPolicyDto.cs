using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketGroupPolicyDto
    {
        [DataMember(Name = "id")]
        public int id { get; set; }
        
        [DataMember(Name = "groupName")]
        public string groupName { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "statusId")]
        public int statusId { get; set; }

        [DataMember(Name = "limitAccess")]
        public string limitAccess { get; set; }

        [DataMember(Name = "limitAccessId")]
        public int limitAccessId { get; set; }

        [DataMember(Name = "newTicketNotify")]
        public string newTicketNotify { get; set; }

        [DataMember(Name = "newTicketNotifyId")]
        public int newTicketNotifyId { get; set; }

        [DataMember(Name = "assignedNotify")]
        public string assignedNotify { get; set; }

        [DataMember(Name = "assignedNotifyId")]
        public int assignedNotifyId { get; set; }

        [DataMember(Name = "replyNotify")]
        public string replyNotify { get; set; }

        [DataMember(Name = "replyNotifyId")]
        public int replyNotifyId { get; set; }

        [DataMember(Name = "changeStatusNotify")]
        public string changeStatusNotify { get; set; }

        [DataMember(Name = "changeStatusNotifyId")]
        public int changeStatusNotifyId { get; set; }

        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime createdDate { get; set; }

        [DataMember(Name = "modifiedDate")]
        public DateTime modifiedDate { get; set; }

        [DataMember(Name = "createTicket")]
        public bool createTicket { get; set; }

        [DataMember(Name = "subTicket")]
        public bool subTicket { get; set; }

        [DataMember(Name = "editTicket")]
        public bool editTicket { get; set; }

        [DataMember(Name = "editRequestor")]
        public bool editRequestor { get; set; }

        [DataMember(Name = "postTicket")]
        public bool postTicket { get; set; }

        [DataMember(Name = "closeTicket")]
        public bool closeTicket { get; set; }

        [DataMember(Name = "assignTicket")]
        public bool assignTicket { get; set; }

        [DataMember(Name = "mergeTicket")]
        public bool mergeTicket { get; set; }

        [DataMember(Name = "deleteTicket")]
        public bool deleteTicket { get; set; }

        [DataMember(Name = "deptTransfer")]
        public bool deptTransfer { get; set; }

        [DataMember(Name = "changeStatus")]
        public bool changeStatus { get; set; }

        [DataMember(Name = "reportAccessId")]
        public int reportAccessId { get; set; }

        [DataMember(Name = "reportAccess")]
        public string reportAccess { get; set; }


        public IEnumerable<GroupPolicyAssignTeamListDto> assignTeamList { get; set; }
        public IEnumerable<GroupPolicyAssignTeamListDto> assignReportLimitAccessTeamList { get; set; }

    }
}
