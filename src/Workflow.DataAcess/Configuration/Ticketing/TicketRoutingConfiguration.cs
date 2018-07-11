using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketRoutingConfiguration : AbstractModelConfigurationLevel2<TicketRouting>
    {
        public TicketRoutingConfiguration() : base()
        {
            this.ToTable("ROUTING", "TICKET");
        
            this.Property(t => t.TicketAssignId).HasColumnName("TICKET_ASSIGN_ID");
            this.Property(t => t.EmpId).HasColumnName("EMP_ID");
            this.Property(t => t.Status).HasColumnName("STATUS");
        }
    }
}
