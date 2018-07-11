


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.Name)
                .HasMaxLength(100);


            // Table & Column Configurationpings

            this.ToTable("ROLES", "ADMIN");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.Name).HasColumnName("NAME");

            this.Property(t => t.Active).HasColumnName("ACTIVE");

            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");

        }
    }
}
