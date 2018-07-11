using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.Ticketing.Dto
{
    public class ChangeStatusActParams : AbstractTicketParam
    {
        public int StatusId { get; set; }
        public decimal? ActualMinutes { get; set; }
        public bool closeK2Form { get; set; }
        public int RootCauseId { get; set; }
    }
}
