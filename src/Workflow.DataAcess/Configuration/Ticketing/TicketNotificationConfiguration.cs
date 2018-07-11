using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketNotificationConfiguration : AbstractModelConfiguration<TicketNotification>
    {
        public TicketNotificationConfiguration()
        {
            this.ToTable("NOTIFICATION", "TICKET");
            this.Property(t => t.ActivityId).HasColumnName("ACTIVITY_ID");
            this.Property(t => t.NotificationType).HasColumnName("NOTIFICATION_TYPE");
            this.Property(t => t.EmpId).HasColumnName("EMP_ID");
            this.Property(t => t.Status).HasColumnName("STATUS");
        }
    }
}
