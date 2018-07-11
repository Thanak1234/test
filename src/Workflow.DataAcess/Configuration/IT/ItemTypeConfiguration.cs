using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.IT;

namespace Workflow.DataAcess.Configuration.IT
{
    public class ItemTypeConfiguration : EntityTypeConfiguration<ItemType>
    {
        public ItemTypeConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);

            // Table & Column Mappings

            this.ToTable("ITEM_TYPE", "IT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.ItemTypeName).HasColumnName("ITEM_TYPE_NAME");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");
            this.Property(t => t.Version).HasColumnName("VERSION");
        }
    }
}
