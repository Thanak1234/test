using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.IT;

namespace Workflow.DataAcess.Configuration.IT
{
    public class ItemConfiguration : EntityTypeConfiguration<Item>
    {
        public ItemConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);

            // Table & Column Mappings

            this.ToTable("ITEM", "IT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.SessionId).HasColumnName("DEPT_SESSION_ID");
            this.Property(t => t.ItemName).HasColumnName("REQUEST_ITEM_NAME");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");
            this.Property(t => t.HodRequired).HasColumnName("HOD_REQUIRED");
            this.HasRequired<Session>(p => p.Session).WithMany().HasForeignKey(p => p.SessionId);
        }
    }
}
