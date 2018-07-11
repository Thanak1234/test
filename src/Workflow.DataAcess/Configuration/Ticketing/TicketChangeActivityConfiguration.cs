using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketChangeActivityConfiguration : AbstractModelConfiguration<TicketChangeActivity>
    {
        public TicketChangeActivityConfiguration() : base()
        {
            this.ToTable("STATUS_ACTIVITY", "TICKET");
            this.Property(t => t.ActivityId).HasColumnName("ACTIVITY_ID");
            this.Property(t => t.statusFromId).HasColumnName("STATUS_FROM_ID");
            this.Property(t => t.statusToId).HasColumnName("STATUS_TO_ID");
            this.Property(t => t.RootCauseId).HasColumnName("ROOT_CAUSE_ID");
        }
    }
}
