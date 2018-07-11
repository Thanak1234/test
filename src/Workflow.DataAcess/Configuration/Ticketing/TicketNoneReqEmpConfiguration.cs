using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketNoneReqEmpConfiguration : AbstractModelConfiguration<TicketNoneReqEmp>
    {
        public TicketNoneReqEmpConfiguration()
        {
            this.ToTable("TICKET_NONE_REG_EMP", "TICKET");
            this.Property(t => t.TicketId).HasColumnName("TICKET_ID");
            this.Property(t => t.EmailItemId).HasColumnName("EMAIL_ITEM_ID");
            this.Property(t => t.Originator).HasColumnName("ORIGINATOR");
            this.Property(t => t.Receipient).HasColumnName("RECEIPIENT");
            this.Property(t => t.Cc).HasColumnName("CC");
        }
    }
}
