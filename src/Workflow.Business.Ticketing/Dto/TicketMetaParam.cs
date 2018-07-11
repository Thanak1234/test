using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.Ticketing.Dto
{
    public class TicketMetaParam
    {

        //Ticket information

        public int DeptOwnerId { get; set; }
        public int TicketItemId { get; set; }
        public int TicketTypeId { get; set; }
        public int RequestorId { get; set; }
        public int SiteId { get; set; }
        public int ImpactId { get; set; }
        public int UrgencyId { get; set; }
        public int PriorityId { get; set; }
        public int SourceId { get; set; }

        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
