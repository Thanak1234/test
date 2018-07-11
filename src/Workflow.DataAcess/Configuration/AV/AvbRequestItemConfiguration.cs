using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.AV;

namespace Workflow.DataAcess.Configuration.AV
{
    public class AvbRequestItemConfiguration : EntityTypeConfiguration<AvbRequestItem>
    {
        public AvbRequestItemConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            // Table & Column Mappings

            this.ToTable("REQUEST_ITEM", "EVENT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.ItemId).HasColumnName("ITEM_ID");
            this.Property(t => t.Quantity).HasColumnName("QTY");
            this.Property(t => t.Comment).HasColumnName("COMMENT");
            this.HasRequired<RequestHeader>(p => p.RequestHeader).WithMany().HasForeignKey(p => p.RequestHeaderId);
            this.HasRequired<AvbItem>(p => p.Item).WithMany().HasForeignKey(p => p.ItemId);
        }
    }

}