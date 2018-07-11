


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class WorkflowRoleConfiguration : EntityTypeConfiguration<WorkflowRole>
    {
        public WorkflowRoleConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.Name)
                .HasMaxLength(250);


            this.Property(t => t.Value)
                .HasMaxLength(250);


            // Table & Column Configurationpings

            this.ToTable("WORKFLOW_ROLE", "ADMIN");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.ParentId).HasColumnName("PARENT_ID");

            this.Property(t => t.Name).HasColumnName("NAME");

            this.Property(t => t.Value).HasColumnName("VALUE");

            this.Property(t => t.Active).HasColumnName("ACTIVE");

            this.Property(t => t.IconIndex).HasColumnName("ICON_INDEX");

        }
    }
}
