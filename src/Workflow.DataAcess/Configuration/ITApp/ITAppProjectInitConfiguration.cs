using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities.Core.ITApp;

namespace Workflow.DataAcess.Configuration.ITApp 
{
    public class ITAppProjectInitConfiguration : EntityTypeConfiguration<ItappProjectInit>
    {
        public ITAppProjectInitConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Application)
                .HasMaxLength(250);

            this.Property(t => t.ProposedChange)
                .HasMaxLength(500);

            this.Property(t => t.BenefitCS);
            this.Property(t => t.BenefitIIS);
            this.Property(t => t.BenefitRM);

            this.Property(t => t.BenefitOther)
                .HasMaxLength(500);

            this.Property(t => t.Description)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("ITAPP_PROJECT_INIT", "IT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.Application).HasColumnName("APPLICATION");
            this.Property(t => t.ProposedChange).HasColumnName("PROPOSED_CHANGE");
            this.Property(t => t.BenefitCS).HasColumnName("BENEFIT_CS");
            this.Property(t => t.BenefitIIS).HasColumnName("BENEFIT_IIS");
            this.Property(t => t.BenefitRM).HasColumnName("BENEFIT_RM");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");
            this.Property(t => t.BenefitOther).HasColumnName("BENEFIT_OTHER");
            this.Property(t => t.PriorityConsideration).HasColumnName("PRIORITY_CONSIDERATION");
        }
    }
}
