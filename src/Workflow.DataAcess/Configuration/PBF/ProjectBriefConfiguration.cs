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
    public class ProjectBriefConfiguration : EntityTypeConfiguration<ProjectBrief>
    {
        public ProjectBriefConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            
            // Table & Column Mappings
            this.ToTable("PROJECT_BRIEF", "EVENT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.ProjectName).HasColumnName("PROJECT_NAME");
            this.Property(t => t.BusinessUnit).HasColumnName("BUSINESS_UNIT");
            this.Property(t => t.ProjectLead).HasColumnName("PROJECT_LEAD");
            this.Property(t => t.AccountManager).HasColumnName("ACCOUNT_MANAGER");
            this.Property(t => t.Budget).HasColumnName("BUDGET");
            this.Property(t => t.BillingInfo).HasColumnName("BILLING_INFO");
            this.Property(t => t.SubmissionDate).HasColumnName("SUBMISSION_DATE");
            this.Property(t => t.RequiredDate).HasColumnName("REQUIRED_DATE");
            this.Property(t => t.ActualEventDate).HasColumnName("ACTUAL_EVENT_DATE");
            this.Property(t => t.Introduction).HasColumnName("INTRODUCTION");
            this.Property(t => t.TargetMarket).HasColumnName("TARGET_MARKET");
            this.Property(t => t.Usage).HasColumnName("USAGE");
            this.Property(t => t.Briefing).HasColumnName("BRIEFING");
            this.Property(t => t.DesignDuration).HasColumnName("DESIGN_DURATION");
            this.Property(t => t.ProductDuration).HasColumnName("PRODUCT_DURATION");
            this.Property(t => t.Dateline).HasColumnName("DATELINE");
            this.Property(t => t.BrainStorm).HasColumnName("BRAIN_STORM");
            this.Property(t => t.ProjectStart).HasColumnName("PROJECT_START");
            this.Property(t => t.FirstRevision).HasColumnName("FIRST_REVISION");
            this.Property(t => t.SecondRevision).HasColumnName("SECOND_REVISION");
            this.Property(t => t.FinalApproval).HasColumnName("FINAL_APPROVAL");
            this.Property(t => t.Completion).HasColumnName("COMPLETION");
            this.Property(t => t.ProjectReference).HasColumnName("PROJECT_REFERENCE");
            this.Property(t => t.DraftComment).HasColumnName("DRAFT_COMMENT");

            this.HasRequired<RequestHeader>(p => p.RequestHeader).WithMany().HasForeignKey(p => p.RequestHeaderId);
            
            /****** Script for SelectTopNRows command from SSMS  ******/
        }
    }
}