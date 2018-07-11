using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketGroupPolicyTeamAssignConfiguration : AbstractModelConfigurationLevel2<TicketGroupPolicyTeamAssign>
    {
        public TicketGroupPolicyTeamAssignConfiguration() : base()
        {
            this.ToTable("TEAM_GROUP_ACCESS", "TICKET");
            this.Property(t => t.TeamId).HasColumnName("TEAM_ID");
            this.Property(t => t.Status).HasColumnName("STATUS");
            this.Property(t => t.GroupPolicyId).HasColumnName("GROUP_ACCESS_ID");
            
        }
    }
}
