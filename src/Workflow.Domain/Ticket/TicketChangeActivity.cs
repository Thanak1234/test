using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketChangeActivity : AbstractBaseEntity 
    {
        public int ActivityId { get; set; }
        public int statusFromId { get; set; }
        public int statusToId { get; set; }
        public int RootCauseId { get; set; }
    }
}
