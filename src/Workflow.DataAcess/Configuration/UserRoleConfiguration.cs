


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.UserName)
                .HasMaxLength(250);


            // Table & Column Configurationpings

            this.ToTable("USER_ROLE", "ADMIN");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.UserName).HasColumnName("USER_NAME");

            this.Property(t => t.RoleId).HasColumnName("ROLE_ID");

            this.Property(t => t.IsRoleRightAdmin).HasColumnName("IS_ROLE_RIGHT_ADMIN");

            this.Property(t => t.IsRoleRightAssign).HasColumnName("IS_ROLE_RIGHT_ASSIGN");

            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");

        }
    }
}
