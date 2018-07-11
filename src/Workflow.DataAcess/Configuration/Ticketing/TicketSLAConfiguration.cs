using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketSLAConfiguration : AbstractModelConfigurationLevel2<TicketSLA>
    {
        public TicketSLAConfiguration():base(){
            this.ToTable("SLA", "TICKET");
            this.Property(t => t.SlaName).HasColumnName("SLA_NAME");
            this.Property(t => t.GracePeriod).HasColumnName("GRACE_PERIOD");
            this.Property(t => t.FirstResponsePeriod).HasColumnName("FIRST_RESPONSE_PERIOD");            
            this.Property(t => t.Status).HasColumnName("STATUS");
        }
    }
}
