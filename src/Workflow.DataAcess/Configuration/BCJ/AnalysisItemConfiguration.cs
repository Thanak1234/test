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
    public class AnalysisItemConfiguration : EntityTypeConfiguration<AnalysisItem>
    {
        public AnalysisItemConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("BCJ_ANALYSIS", "FINANCE");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.ProjectDetailId).HasColumnName("BCJ_ID");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");
            this.Property(t => t.Revenue).HasColumnName("REVENUE");
            this.Property(t => t.Quantity).HasColumnName("QUANTITY");
            this.Property(t => t.Total).HasColumnName("TOTAL");
            this.HasRequired<ProjectDetail>(p => p.ProjectDetail).WithMany().HasForeignKey(p => p.ProjectDetailId);
        }
    }

}