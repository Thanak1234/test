using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketMergedConfiguration : AbstractModelConfiguration<TicketMerged>
    {
        public TicketMergedConfiguration()
        {
            this.ToTable("MERGED_TICKET", "TICKET");
            this.Property(t => t.ActivityId).HasColumnName("ACTIVITY_ID");
            this.Property(t => t.ToActivityId).HasColumnName("TO_ACTIVITY_ID");
            this.Property(t => t.ticketId).HasColumnName("TICKET_ID");
            this.Property(t => t.ToTicketId).HasColumnName("TO_TICKET_ID");
        }
    }
}
