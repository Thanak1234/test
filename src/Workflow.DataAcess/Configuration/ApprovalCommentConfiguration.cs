


using System.ComponentModel.DataAnnotations.Schema;

using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Configuration
{
    public class ApprovalCommentConfiguration : EntityTypeConfiguration<ApprovalComment>
    {
        public ApprovalCommentConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.Activity)
                .IsRequired()
                .HasMaxLength(50);


            this.Property(t => t.ApplicationName)
                .HasMaxLength(50);


            this.Property(t => t.Approver)
                .HasMaxLength(70);


            this.Property(t => t.ApproverDisplayName)
                .HasMaxLength(100);


            this.Property(t => t.Decision)
                .IsRequired()
                .HasMaxLength(50);


            this.Property(t => t.Comments)
                .HasMaxLength(255);


            // Table & Column Configurationpings

            this.ToTable("APPROVAL_COMMENT", "BPMDATA");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");

            this.Property(t => t.ActInstId).HasColumnName("ACT_INST_ID");

            this.Property(t => t.Activity).HasColumnName("ACTIVITY");

            this.Property(t => t.ApplicationName).HasColumnName("APPLICATION_NAME");

            this.Property(t => t.Approver).HasColumnName("APPROVER");

            this.Property(t => t.ApproverDisplayName).HasColumnName("APPROVER_DISPLAY_NAME");

            this.Property(t => t.Decision).HasColumnName("DECISION");

            this.Property(t => t.Comments).HasColumnName("COMMENTS");

            this.Property(t => t.Version).HasColumnName("VERSION");

        }
    }
}
