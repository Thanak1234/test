


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class WorkflowRightConfiguration : EntityTypeConfiguration<WorkflowRight>
    {
        public WorkflowRightConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(50);


            this.Property(t => t.WorkflowName)
                .HasMaxLength(50);


            this.Property(t => t.Activity)
                .HasMaxLength(50);


            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);


            this.Property(t => t.ModifiedBy)
                .IsRequired()
                .HasMaxLength(50);


            // Table & Column Configurationpings

            this.ToTable("WORKFLOW_RIGHT", "BPMDATA");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.UserName).HasColumnName("USER_NAME");

            this.Property(t => t.WorkflowName).HasColumnName("WORKFLOW_NAME");

            this.Property(t => t.Activity).HasColumnName("ACTIVITY");

            this.Property(t => t.WorkflowAdmin).HasColumnName("WORKFLOW_ADMIN");

            this.Property(t => t.CreatedBy).HasColumnName("CREATED_BY");

            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");

            this.Property(t => t.ModifiedBy).HasColumnName("MODIFIED_BY");

            this.Property(t => t.ModifiedDate).HasColumnName("MODIFIED_DATE");

            this.Property(t => t.Version).HasColumnName("VERSION");

        }
    }
}
