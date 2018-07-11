using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.DataAcess.Configuration
{
    public class LookupConfiguration : EntityTypeConfiguration<Lookup>
    {
        public LookupConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            
            // Table & Column Mappings
            this.ToTable("LOOKUP", "FORM");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.ParentId).HasColumnName("PARENT_ID");
            this.Property(t => t.FormId).HasColumnName("FORM_ID");
            this.Property(t => t.Name).HasColumnName("NAMESPACE");
            this.Property(t => t.Value).HasColumnName("LOOKUP_VALUE");
            this.Property(t => t.Sequence).HasColumnName("SEQUENCE");
            this.Property(t => t.Active).HasColumnName("ACTIVE");

            this.HasRequired<Lookup>(p => p.Parent).WithMany().HasForeignKey(p => p.ParentId);

            this.Ignore(t => t.HasChild);
        }
    }
}