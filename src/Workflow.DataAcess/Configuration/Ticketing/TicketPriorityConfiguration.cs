using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketPriorityConfiguration : AbstractModelConfigurationLevel2<TicketPriority>
    {
        public TicketPriorityConfiguration() : base()
        {
            // Table & Column Configurationpings
            this.ToTable("PRIORITY", "TICKET");
            this.Property(t => t.PriorityName).HasColumnName("PRIORITY_NAME");
            this.Property(t => t.SlaId).HasColumnName("SLA_ID");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");
        }
         
    }
}
