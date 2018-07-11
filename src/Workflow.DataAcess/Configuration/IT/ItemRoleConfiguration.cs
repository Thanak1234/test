using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.IT;

namespace Workflow.DataAcess.Configuration.IT
{
    public class ItemRoleConfiguration : EntityTypeConfiguration<ItemRole>
    {
        public ItemRoleConfiguration() {
            // Primary Key

            this.HasKey(t => t.Id);

            // Table & Column Mappings

            this.ToTable("ITEM_ROLE", "IT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RoleName).HasColumnName("ROLE_NAME");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");
            this.Property(t => t.Version).HasColumnName("VERSION");
            this.Property(t => t.IsAdmin).HasColumnName("ADMIN");
        }
    }
}
