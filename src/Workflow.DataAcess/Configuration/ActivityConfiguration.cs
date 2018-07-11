


using System.ComponentModel.DataAnnotations.Schema;

using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities.Core;

namespace Workflow.DataAcess.Configuration
{
    public class ActivityConfiguration : EntityTypeConfiguration<Activity>
    {
        public ActivityConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.Type)
                .HasMaxLength(100);
            

            this.Property(t => t.Name)
                .HasMaxLength(250);


            this.Property(t => t.DisplayName)
                .HasMaxLength(100);


            this.Property(t => t.ActCode)
                .HasMaxLength(50);


            // Table & Column Configurationpings

            this.ToTable("ACTIVITY", "ADMIN");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.WorkflowId).HasColumnName("WORKFLOW_ID");

            this.Property(t => t.Type).HasColumnName("TYPE");
            
            this.Property(t => t.Name).HasColumnName("NAME");

            this.Property(t => t.DisplayName).HasColumnName("DISPLAY_NAME");

            this.Property(t => t.ActCode).HasColumnName("ACT_CODE");
            
            this.Property(t => t.Property).HasColumnName("PROPERTY");
            
            this.Property(t => t.Sequence).HasColumnName("SEQUENCE");

            this.Property(t => t.Active).HasColumnName("ACTIVE");

        }
    }
}
