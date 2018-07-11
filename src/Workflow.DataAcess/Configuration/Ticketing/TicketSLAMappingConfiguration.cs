using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketSLAMappingConfiguration : AbstractModelConfigurationLevel2<TicketSLAMapping>
    {
        public TicketSLAMappingConfiguration():base(){
            this.ToTable("SLA_MAPPING", "TICKET");
            this.Property(t => t.SlaId).HasColumnName("SLA_ID");
            this.Property(t => t.TypeId).HasColumnName("TYPE_ID");
            this.Property(t => t.PriorityId).HasColumnName("PRIORITY_ID");                        
        }
    }
}
