using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketTeamAgentAssignConfiguration : AbstractModelConfigurationLevel2<TicketTeamAgentAssign>
    {
        public TicketTeamAgentAssignConfiguration() : base()
        {
            this.ToTable("TEAM_AGENT_ASSIGN", "TICKET");
            this.Property(t => t.TeamId).HasColumnName("TEAM_ID");
            this.Property(t => t.Status).HasColumnName("STATUS");
            this.Property(t => t.AgentId).HasColumnName("AGENT_ID");
            this.Property(t => t.ImmediateAssign).HasColumnName("IMMEDIATE_ASSIGN");
            
        }
    }
}
