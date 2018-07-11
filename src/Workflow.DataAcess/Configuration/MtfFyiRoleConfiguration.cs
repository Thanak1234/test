


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class MtfFyiRoleConfiguration : EntityTypeConfiguration<MtfFyiRole>
    {
        public MtfFyiRoleConfiguration()
        {
            // Primary Key

            this.HasKey(t => new { t.Id, t.Fqn });


            // Properties

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            this.Property(t => t.Guid)
                .HasMaxLength(36);


            this.Property(t => t.Fqn)
                .IsRequired()
                .HasMaxLength(448);


            this.Property(t => t.Description)
                .HasMaxLength(255);


            // Table & Column Configurationpings

            this.ToTable("MTF_FYI_ROLE");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.Guid).HasColumnName("Guid");

            this.Property(t => t.Fqn).HasColumnName("FQN");

            this.Property(t => t.Description).HasColumnName("Description");

        }
    }
}
