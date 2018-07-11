using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketTypeConfiguration : AbstractModelConfigurationLevel2<TicketType>
    {
        public TicketTypeConfiguration() : base()
        {
            // Table & Column Configurationpings

            this.ToTable("TYPE", "TICKET");
            this.Property(t => t.TypeName).HasColumnName("TYPE_NAME");
            this.Property(t => t.Icon).HasColumnName("ICON");
        }
    }
}
