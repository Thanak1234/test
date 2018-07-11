


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class EscalationConfiguration : EntityTypeConfiguration<Escalation>
    {
        public EscalationConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.AppCode)
                .IsRequired()
                .HasMaxLength(100);


            this.Property(t => t.ActName)
                .IsRequired()
                .HasMaxLength(255);


            this.Property(t => t.UserRoleName)
                .HasMaxLength(100);


            this.Property(t => t.Type)
                .HasMaxLength(20);


            this.Property(t => t.DatePart)
                .IsRequired()
                .HasMaxLength(10);


            // Table & Column Configurationpings

            this.ToTable("ESCALATION", "BPMDATA");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.AppCode).HasColumnName("APP_CODE");

            this.Property(t => t.ActName).HasColumnName("ACT_NAME");

            this.Property(t => t.UserRoleName).HasColumnName("USER_ROLE_NAME");

            this.Property(t => t.Type).HasColumnName("TYPE");

            this.Property(t => t.EscalateAfter).HasColumnName("ESCALATE_AFTER");

            this.Property(t => t.DatePart).HasColumnName("DATE_PART");

            this.Property(t => t.Priority).HasColumnName("PRIORITY");

            this.Property(t => t.Repeat).HasColumnName("REPEAT");

            this.Property(t => t.Active).HasColumnName("ACTIVE");

        }
    }
}
