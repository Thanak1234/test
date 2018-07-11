using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.Ticketing.Dto
{
    public class TicketHeaderParam
    {
        public int TicketId { get; set; }
        public int CurrUserId { get; set; }
        public string ActivityCode { get; set; }
    }
}
