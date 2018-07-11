


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class RequestExportedConfiguration : EntityTypeConfiguration<RequestExported>
    {
        public RequestExportedConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.RequestHeaderId);


            // Properties

            this.Property(t => t.RequestHeaderId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


            this.Property(t => t.RequestCode)
                .IsRequired()
                .HasMaxLength(20);


            // Table & Column Configurationpings

            this.ToTable("REQUEST_EXPORTED", "BPMDATA");

            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");

            this.Property(t => t.RequestCode).HasColumnName("REQUEST_CODE");

            this.Property(t => t.Exported).HasColumnName("EXPORTED");

        }
    }
}
