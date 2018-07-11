using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;

namespace Workflow.Business.Ticketing.Dto
{
    public class TicketParams : AbstractTicketParam
    {
        public enum INTEGRATED_TYPE { NONE,  EMAIL, K2}

        public int DeptOwnerId { get; set; }
        public int TicketItemId { get; set; }
        public int TicketTypeId { get; set; }
        public int RequestorId { get; set; }
        public int SiteId { get; set; }
        public int StatusId { get; set; }
        public int ImpactId { get; set; }
        public int UrgencyId { get; set; }
        public int PriorityId { get; set; }
        public int SourceId { get; set; }

        public int TeamId { get; set; }
        public int Assignee { get; set; }

        public string Subject { get; set; }
        public string Description { get; set; }

        public decimal? EstimatedHours { get; set; }
        public DateTime? DueDate { get; set; }

        public List<FileUploadInfo> UserAttachFiles { get; set; }
        public List<FileUploadInfo> UserAttachFilesDel { get; set; }

        public TicketNoneReqEmpDto TicketNoneReqEmp { get; set; }

        public INTEGRATED_TYPE AutomationType { get; set; } = INTEGRATED_TYPE.NONE;
        public int SlaId { get; set; }

        public string RootCause { get; set; }

        public string RefType { get; set; }
        public int? Reference { get; set; }
    }
}
