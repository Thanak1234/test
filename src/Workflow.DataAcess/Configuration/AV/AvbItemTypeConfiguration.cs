using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.AV;

namespace Workflow.DataAcess.Configuration.AV
{
    public class AvbItemTypeConfiguration : EntityTypeConfiguration<AvbItemType>
    {
        public AvbItemTypeConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);

            // Table & Column Mappings

            this.ToTable("ITEM_TYPE", "EVENT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.ItemTypeName).HasColumnName("ITEM_TYPE_NAME");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");


        }
    }
}
