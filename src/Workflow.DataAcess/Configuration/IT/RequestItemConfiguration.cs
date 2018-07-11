using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.IT;

namespace Workflow.DataAcess.Configuration.IT
{
    public class RequestItemConfiguration : EntityTypeConfiguration<RequestItem>
    {
       public RequestItemConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);

            // Table & Column Mappings

            this.ToTable("REQUEST_ITEM", "IT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQ_HEADER_ID");
            this.Property(t => t.ItemId).HasColumnName("REQ_ITEM_ID");
           
            this.Property(t => t.ItemTypeId).HasColumnName("REQ_ITEM_TYPE_ID");
            this.Property(t => t.ItemRoleId).HasColumnName("REQ_ITEM_ROLE_ID");
            this.Property(t => t.Qty).HasColumnName("QTY");
            this.Property(t => t.Comment).HasColumnName("COMMENT");
            this.Property(t => t.Version).HasColumnName("VERSION");

            this.HasRequired<Item>(p => p.Item).WithMany().HasForeignKey(p => p.ItemId);
            this.HasRequired<ItemRole>(p => p.ItemRole).WithMany().HasForeignKey(p => p.ItemRoleId);
            this.HasRequired<ItemType>(p => p.ItemType).WithMany().HasForeignKey(p => p.ItemTypeId);

            this.HasRequired<RequestHeader>(p => p.RequestHeader).WithMany().HasForeignKey(p=>p.RequestHeaderId);
        }
    }
}
