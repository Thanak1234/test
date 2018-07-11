using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities.Core.ITApp;

namespace Workflow.DataAcess.Configuration.ITApp 
{
    public class ITAppProjectApprovalConfiguration : EntityTypeConfiguration<ItappProjectApproval>
    {
        public ITAppProjectApprovalConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Rsim)
                .HasMaxLength(500);

            this.Property(t => t.Rawm)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ITAPP_PROJECT_APPROVAL", "IT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.Hc).HasColumnName("HC");
            this.Property(t => t.Slc).HasColumnName("SLC");
            this.Property(t => t.Scmd).HasColumnName("SCMD");
            this.Property(t => t.Rsim).HasColumnName("RSIM");
            this.Property(t => t.Rawm).HasColumnName("RAWM");
            this.Property(t => t.DeliveryDate).HasColumnName("DELIVERY_DATE");
            this.Property(t => t.GoLiveDate).HasColumnName("GO_LIVE_DATE");
        }
    }
}
