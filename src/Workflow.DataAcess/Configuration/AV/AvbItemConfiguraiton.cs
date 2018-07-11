using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.AV;

namespace Workflow.DataAcess.Configuration.AV
{
    public class AvbItemConfiguraiton : EntityTypeConfiguration<AvbItem>
    {
        public AvbItemConfiguraiton()
        {
            // Primary Key

            this.HasKey(t => t.Id);

            // Table & Column Mappings

            this.ToTable("ITEM", "EVENT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.ItemName).HasColumnName("ITEM_NAME");
            this.Property(t => t.ItemTypeId).HasColumnName("ITEM_TYPE_ID");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");

            this.HasRequired<AvbItemType>(p => p.ItemType).WithMany().HasForeignKey(p => p.ItemTypeId);
        }
    }
}
