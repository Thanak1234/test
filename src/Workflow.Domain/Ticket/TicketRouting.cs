using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketRouting : AbstractEntityLevel2
    {
        public const string AVAILABLE   = "AVAILABLE";
        public const string RESPONSED   = "RESPONSED";
        public const string ASSIGNED    = "ASSIGNED";
        public const string EXPIRED     = "EXPIRED";

        public int TicketAssignId { get; set; }
        public int EmpId { get; set; }
        public string Status { get; set; } = AVAILABLE;
    }
}
