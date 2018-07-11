using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketingSearchParamsDto {
        public List<int?> TypeId { get; set; }
        public string ticketNo { get; set; }
        public string subject { get; set; }
        public List<int?> StatusId { get; set; }
        public int? SourceId { get; set; }
        public List<int?> Priority { get; set; }
        public List<int?> TeamId { get; set; }
        public List<int?> assignedId { get; set; }
        public List<int?> CateId { get; set; }
        public List<int?> SubCateId { get; set; }
        public List<int?> ItemId { get; set; }
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

        public List<int?> RootCauseId { get; set; }

        public DateTime? SubmittedDateFrom { get; set; }
        public DateTime? SubmittedDateTo { get; set; }

        public List<int?> SlaId { get; set; }

        public bool IncludeRemoved { get; set; }

        public string description { get; set; }
        public string comment { get; set; }
    }
}
