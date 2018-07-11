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
    public class ProjectDetailConfiguration : EntityTypeConfiguration<ProjectDetail>
    {
        public ProjectDetailConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("BCJ", "FINANCE");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.CapexCategoryId).HasColumnName("CAPEX_CATEGORY_ID");
            this.Property(t => t.ProjectName).HasColumnName("PROJECT_NAME");
            this.Property(t => t.Reference).HasColumnName("REFERENCE");
            this.Property(t => t.CoporationBranch).HasColumnName("COPORATION_BRANCH");
            this.Property(t => t.WhatToDo).HasColumnName("WHAT_TODO");
            this.Property(t => t.WhyToDo).HasColumnName("WHY_TODO");
            this.Property(t => t.Arrangement).HasColumnName("ARRANGEMENT");
            this.Property(t => t.TotalAmount).HasColumnName("TOTAL_AMOUNT");
            this.Property(t => t.EstimateCapex).HasColumnName("ESTIMATED_CAPEX");
            this.Property(t => t.IncrementalNetContribution).HasColumnName("INCREMENTAL_NET_CONTRIBUTION");
            this.Property(t => t.IncrementalNetEbita).HasColumnName("INCREMENTAL_NET_EBITDA");
            this.Property(t => t.PaybackYear).HasColumnName("PAYBACK_YEAR");
            this.Property(t => t.OutlineBenefit).HasColumnName("OUTLINE_BENEFIT");
            this.Property(t => t.Commencement).HasColumnName("COMMENCEMENT");
            this.Property(t => t.Completion).HasColumnName("COMPLETION");
            this.Property(t => t.Alternative).HasColumnName("ALTERNATIVE");
            this.Property(t => t.OutlineRisk).HasColumnName("OUTLINE_RISK");
            this.Property(t => t.CapitalRequired).HasColumnName("CAPITAL_REQUIRED");
            this.Property(t => t.ModifiedDate).HasColumnName("MODIFIED_DATE");
            this.HasRequired<RequestHeader>(p => p.RequestHeader).WithMany().HasForeignKey(p => p.RequestHeaderId);
            this.HasRequired<CapexCategory>(p => p.CapexCategory).WithMany().HasForeignKey(p => p.CapexCategoryId);
        }
    }

}