using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class Ticket : AbstractEntityLevel2
    {

        public string TicketNo { get; set; }

        public int TicketTypeId { get; set; }

        public int DeptId { get; set; }

        public int SubmitterId { get; set; }
        public int RequestorId { get; set; }

        public int PriorityId { get; set; }
        public int UrgentcyId { get; set; }
        public int ImpactId { get; set; }

        public int SourceId { get; set; }
        public int SiteId { get; set; }
        public int TicketItemId { get; set; }
        public string Subject { get; set; }
        
        public int StatusId { get; set; }

        public int LastActionBy { get; set; }
        public string LastAction { get; set; }

        public DateTime? LastActionDate { get; set; }

        public string WaitActionedBy { get; set; } = "AGENT";
        public int LastAssEmpId { get; set; }
        public int LastAssTeamId { get; set; }
       
        public DateTime? DueDate { get; set; }

        public decimal? EstimatedHours { get; set; }
        public decimal? actualMinutes { get; set; }

        public DateTime? completedDate { get; set; }

        public bool FirstResponse { get; set; }

        public DateTime? FirstResponseDate { get; set; }

        public int LockedBy { get; set; }

        public int SlaId { get; set; }

        public int? RootCauseId { get; set; }

        public string RefType { get; set; }

        public int? Reference { get; set; }
    }
}
