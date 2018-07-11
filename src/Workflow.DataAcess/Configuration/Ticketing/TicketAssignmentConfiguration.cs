using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketAssignmentConfiguration : AbstractModelConfigurationLevel2<TicketAssignment>
    {

        public TicketAssignmentConfiguration() : base()
        {
            this.ToTable("ASSIGNMENT", "TICKET");
            this.Property(t => t.TicketActivityId).HasColumnName("ACTIVITY_ID");
            this.Property(t => t.TeamId).HasColumnName("TEAM_ID");
            this.Property(t => t.AssigneeId).HasColumnName("ASSIGNEE_ID");
            this.Property(t => t.Expired).HasColumnName("EXPIRED");

        }
        
    }
}
