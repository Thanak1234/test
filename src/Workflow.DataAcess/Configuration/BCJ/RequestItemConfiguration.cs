using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.BCJ;

namespace Workflow.DataAcess.Configuration.BCJ
{
    public class RequestItemConfiguration : EntityTypeConfiguration<BcjRequestItem>
    {
        public RequestItemConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("BCJ_DETAIL", "FINANCE");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.ProjectDetailId).HasColumnName("BCJ_ID");
            this.Property(t => t.Item).HasColumnName("ITEM");
            this.Property(t => t.UnitPrice).HasColumnName("UNIT_PRICE");
            this.Property(t => t.Quantity).HasColumnName("QUANTITY");
            this.Property(t => t.Total).HasColumnName("TOTAL");
            this.HasRequired<ProjectDetail>(p => p.ProjectDetail).WithMany().HasForeignKey(p => p.ProjectDetailId);
        }
    }

}