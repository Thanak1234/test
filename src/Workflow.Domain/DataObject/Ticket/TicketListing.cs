using System;
using System.Runtime.Serialization;

namespace Workflow.DataObject.Ticket
{
    [DataContract]
    public class TicketListing
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "ticketNo")]
        public string TicketNo { get; set; }
        [DataMember(Name = "subject")]
        public string Subject { get; set; }
        [DataMember(Name = "createdDate")]
        public DateTime CreatedDate { get; set; }
        [DataMember(Name = "requestor")]
        public string Requestor { get; set; }
        [DataMember(Name = "assignee")]
        public string Assignee { get; set; }

        [DataMember(Name = "firstResponseDate")]
        public DateTime? FirstResponseDate { get; set; }

        [DataMember(Name = "dueDate")]
        public DateTime? DueDate { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "statusId")]
        public int StatusId { get; set; }
        
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "priority")]
        public string Priority { get; set; }
        [DataMember(Name = "teamName")]
        public string TeamName { get; set; }
        [DataMember(Name = "lastAction")]
        public string LastAction { get; set; }
        [DataMember(Name = "lastActionBy")]
        public string LastActionBy { get; set; }
        [DataMember(Name = "lastActionDate")]
        public DateTime? LastActionDate { get; set; }
        [DataMember(Name = "waitActionedBy")]
        public string WaitActionedBy { get; set; }

        [DataMember(Name = "actualMinutes")]
        public decimal? ActualMinutes { get; set; }
        
        [DataMember(Name = "statusFlag")]
        public string StatusFlag { get; set; }

        [DataMember(Name = "fsViolence")]
        public bool FSViolence { get; set; }

        [DataMember(Name = "odViolence")]
        public bool ODViolence { get; set; }

        [DataMember(Name = "isMain")]
        public bool IsMain { get; set; }

        [DataMember(Name = "isSub")]
        public bool IsSub { get; set; }

        [DataMember(Name = "sla")]
        public string Sla { get; set; }

        [DataMember(Name ="source")]
        public string Source { get; set; }

        [DataMember(Name ="ticketType")]
        public string TicketType { get; set; }

        [DataMember(Name = "ticketTypeIcon")]
        public string TicketTypeIcon { get; set; }
    }
}
