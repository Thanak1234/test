using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketTeamConfiguration : AbstractModelConfigurationLevel2<TicketTeam>
    {
        public TicketTeamConfiguration() : base()
        {
            this.ToTable("TEAM", "TICKET");
            this.Property(t => t.TeamName).HasColumnName("TEAM_NAME");
            this.Property(t => t.Status).HasColumnName("STATUS");
            this.Property(t => t.AlertAllMembers).HasColumnName("ALERT_ALL_MEMBERS");
            this.Property(t => t.AlertAssignedAgent).HasColumnName("ALERT_ASSIGNED_AGENT");
            this.Property(t => t.DirectoryListing).HasColumnName("DIRECTORY_LISTING");
        }
    }
}
