using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketFormIntegratedConfiguration : AbstractModelConfiguration<TicketFormIntegrated>
    {
        public TicketFormIntegratedConfiguration()
        {
            this.ToTable("E_FORM_INTERGRATED", "TICKET");
            this.Property(t => t.RequestHaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.ActivityId).HasColumnName("ACTIVITY_ID");
            this.Property(t => t.TicketDeptId).HasColumnName("TICKET_DEPT_ID");
            this.Property(t => t.TicketId).HasColumnName("TICKET_ID");
            this.Property(t => t.Status).HasColumnName("STATUS");
        }
    }
}
