using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketAgentConfiguration : AbstractModelConfigurationLevel2<TicketAgent>
    {
        public TicketAgentConfiguration() : base()
        {
            // Table & Column Configurationpings

            this.ToTable("AGENT", "TICKET");
            this.Property(t => t.EmpId).HasColumnName("EMP_ID");
            this.Property(t => t.AccountType).HasColumnName("ACCOUNT_TYPE");
            this.Property(t => t.Status).HasColumnName("STATUS");
            this.Property(t => t.DeptId).HasColumnName("DEPT_ID");
            this.Property(t => t.TimeZone).HasColumnName("TIME_ZONE");
            this.Property(t => t.LimitedAccess).HasColumnName("LIMITED_ACCESS");
            this.Property(t => t.DirectoryListing).HasColumnName("DIRECTORY_LISTING");
            this.Property(t => t.VocationMode).HasColumnName("VACATION_MODE");
            this.Property(t => t.GroupPolicyId).HasColumnName("GROUP_POLICY_ID");
            

        }
    }
}
