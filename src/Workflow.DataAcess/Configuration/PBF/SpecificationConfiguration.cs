using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.PBF;

namespace Workflow.DataAcess.Configuration.PBF
{
    public class SpecificationConfiguration : EntityTypeConfiguration<Specification>
    {
        public SpecificationConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            
            // Table & Column Mappings
            this.ToTable("SPECIFICATION", "EVENT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.ProjectBriefId).HasColumnName("PROJECT_BRIEF_ID");
            this.Property(t => t.ItemId).HasColumnName("ITEM_ID");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");
            this.Property(t => t.Quantity).HasColumnName("QUANTITY");
            this.Property(t => t.ItemReference).HasColumnName("ITEM_REFERENCE");


            this.HasRequired<ProjectBrief>(p => p.ProjectBrief).WithMany().HasForeignKey(p => p.ProjectBriefId);
            this.HasRequired<Lookup>(p => p.Item).WithMany().HasForeignKey(p => p.ItemId);

            this.Ignore(t => t.Name);
            
            /****** Script for SelectTopNRows command from SSMS  ******/
        }
    }
}