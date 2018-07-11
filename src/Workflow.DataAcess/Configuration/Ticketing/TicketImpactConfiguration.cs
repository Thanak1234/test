using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketImpactConfiguration : AbstractModelConfigurationLevel2<TicketImpact>
    {
        public TicketImpactConfiguration():base(){

            // Table & Column Configurationpings
            this.ToTable("IMPACT", "TICKET");
            this.Property(t => t.ImpactName).HasColumnName("IMPACT_NAME");
        }
       
    }
}
