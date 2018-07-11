


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class RoleRightConfiguration : EntityTypeConfiguration<RoleRight>
    {
        public RoleRightConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            // Table & Column Configurationpings

            this.ToTable("ROLE_RIGHT", "ADMIN");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.RoleId).HasColumnName("ROLE_ID");

            this.Property(t => t.RihgtId).HasColumnName("RIHGT_ID");

            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");

        }
    }
}
