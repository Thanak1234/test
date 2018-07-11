using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketActivityConfiguration : AbstractModelConfigurationLevel2<TicketActivity>
    {
        public TicketActivityConfiguration() : base()
        {
            this.ToTable("ACITVITY", "TICKET");
            this.Property(t => t.TicketId).HasColumnName("TICKET_ID");
            this.Property(t => t.ActivityType).HasColumnName("ACTIVITY_TYPE");
            this.Property(t => t.Action).HasColumnName("ACTION");
            this.Property(t => t.ActionBy).HasColumnName("ACTION_BY");
        }
    }
}
