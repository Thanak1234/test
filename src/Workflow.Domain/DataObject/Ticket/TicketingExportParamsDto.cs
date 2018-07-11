using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketingExportParamsDto
    {
        public string TypeId { get; set; }
        public string ticketNo { get; set; }
        public string subject { get; set; }
        public string StatusId { get; set; }
        public int? SourceId { get; set; }
        public string Priority { get; set; }
        public string TeamId { get; set; }
        public string assignedId { get; set; }
        public string CateId { get; set; }
        public string SubCateId { get; set; }
        public string ItemId { get; set; }
        public int? RequestorId { get; set; }
        public DateTime? CompletedDateFrom { get; set; }
        public DateTime? CompletedDateTo { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
        public string Depts { get; set;}
        public int Start { get; set; }
        public int Limit { get; set; }

        public int Page { get; set; }

        public string ExportType { get; set; }

        public int CurrLoginEmpId { get; set; }

        public DateTime? SubmittedDateFrom { get; set; }
        public DateTime? SubmittedDateTo { get; set; }

        public string SlaId { get; set; }

        public bool IncludeRemoved { get; set; }

        public string Sort { get; set; }

        public string Description { get; set; }
        public string Comment { get; set; }
        public string RootCauseId { get; set; }

    }
}
