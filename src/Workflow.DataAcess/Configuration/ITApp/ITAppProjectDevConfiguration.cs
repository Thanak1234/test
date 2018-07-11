using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities.Core.ITApp;

namespace Workflow.DataAcess.Configuration.ITApp 
{
    public class ITAppProjectDevConfiguration : EntityTypeConfiguration<ItappProjectDev>
    {
        public ITAppProjectDevConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Remark)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("ITAPP_PROJECT_DEV", "IT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.StartDate).HasColumnName("START_DATE");
            this.Property(t => t.EndDate).HasColumnName("END_DATE");
            this.Property(t => t.IsQA).HasColumnName("IS_QA");
            this.Property(t => t.Remark).HasColumnName("REMARK");
        }
    }
}
