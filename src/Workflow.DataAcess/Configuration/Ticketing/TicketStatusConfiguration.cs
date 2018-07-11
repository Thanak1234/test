using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketStatusConfiguration : AbstractModelConfigurationLevel2<TicketStatus>
    {
        public TicketStatusConfiguration():base(){

            this.ToTable("STATUS", "TICKET");
            this.Property(t => t.Status).HasColumnName("STATUS");
            this.Property(t => t.StateId).HasColumnName("STATE_ID");
            this.Ignore(t => t.Classify);
        }
    }
}
