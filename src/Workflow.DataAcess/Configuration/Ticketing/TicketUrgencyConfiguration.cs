using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketUrgencyConfiguration : AbstractModelConfigurationLevel2<TicketUrgency>
    {
         public TicketUrgencyConfiguration() : base()
        {
            this.ToTable("URGENCY", "TICKET");
            this.Property(t => t.UrgencyName).HasColumnName("URGENCY_NAME");
        }
    }
}
