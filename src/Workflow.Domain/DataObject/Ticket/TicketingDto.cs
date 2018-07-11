using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketingDto
    {
        public int PagingId { get; set; }
        public int TicketId { get; set; }
        public string TicketNo { get; set; }
        public string RequestorId { get; set; }        
        public string Department { get; set; }        
        public string Requestor { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public string LastActionBy { get; set; }
        public DateTime? LastActionDate { get; set; }
        public string LastAction { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Mode { get; set; }
        public string Impact { get; set; }
        public string Urgency { get; set; }
        public string Priority { get; set; }
        public string Site { get; set; }
        public string Group { get; set; }
        public string LastAssignee { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Item { get; set; }
        public double? EstimatedHours { get; set; }
        public double? ActualMinutes { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Sla { get; set; }
        public string TypeIcon { get; set; }
        public double? CompleteMinutes { get; set; }
        public double? FirstResponseMinutes { get; set; }
        public string TargetCompleteMinutes { get; set; }
        public string TargetFirstResponseMinutes { get; set; }
        public string CloseComment { get; set; }
        public string RootCause { get; set; }
    }


}
