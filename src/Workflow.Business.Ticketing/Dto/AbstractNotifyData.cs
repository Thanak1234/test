using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.Business.Ticketing.Dto
{
    public abstract class AbstractNotifyData
    {

        public TicketActivity Activity { get; set; }
        public List<DestUser> DestUsers { get; set; }
        public Employee ActionBy { get; set; }
        public DateTime ActionDate { get; set; } = DateTime.Now;

        public NotifyMessage NotifyMessage { get; set; }

        public TicketDepartment ticketDept { get; set; }

        public bool IsMail { get; set; }
        public bool IsSMS { get; set; } = false;
        public bool IsUI { get; set; }
    }
}
