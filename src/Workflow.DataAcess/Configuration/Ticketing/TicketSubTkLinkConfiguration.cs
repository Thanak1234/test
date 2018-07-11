using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketSubTkLinkConfiguration : AbstractModelConfiguration<TicketSubTkLink>
    {
        public TicketSubTkLinkConfiguration() :base()
        {
            this.ToTable("SUB_TICKET_LINK", "TICKET");

            this.Property(t => t.ActivityId).HasColumnName("ACTIVITY_ID");
            this.Property(t => t.SubTicketActId).HasColumnName("SUB_TICKET_ACT_ID");
        }
    }
}
