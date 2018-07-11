using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketSourceConfiguration : AbstractModelConfigurationLevel2<TicketSource>
    {
        public TicketSourceConfiguration() : base()
        {
            // Table & Column Configurationpings

            this.ToTable("SOURCE", "TICKET");
            this.Property(t => t.Source).HasColumnName("SOURCE");

        }
    }
}
