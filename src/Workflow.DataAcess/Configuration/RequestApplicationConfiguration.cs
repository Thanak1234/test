


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class RequestApplicationConfiguration : EntityTypeConfiguration<RequestApplication>
    {
        public RequestApplicationConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.RequestCode)
                .HasMaxLength(50);


            this.Property(t => t.RequestDesc)
                .IsRequired()
                .HasMaxLength(200);


            this.Property(t => t.ProcessName)
                .HasMaxLength(200);


            this.Property(t => t.ProcessCode)
                .HasMaxLength(50);


            this.Property(t => t.ProcessPath)
                .HasMaxLength(200);


            this.Property(t => t.FormUrl)
                .HasMaxLength(255);


            // Table & Column Configurationpings

            this.ToTable("REQUEST_APPLICATION", "BPMDATA");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.RequestCode).HasColumnName("REQUEST_CODE");

            this.Property(t => t.RequestDesc).HasColumnName("REQUEST_DESC");

            this.Property(t => t.ProcessName).HasColumnName("PROCESS_NAME");

            this.Property(t => t.ProcessCode).HasColumnName("PROCESS_CODE");

            this.Property(t => t.ProcessPath).HasColumnName("PROCESS_PATH");

            this.Property(t => t.ReportPath).HasColumnName("REPORT_PATH");

            this.Property(t => t.FormUrl).HasColumnName("FORM_URL");

            this.Property(t => t.FormXType).HasColumnName("NONE_SMART_FORM_URL");

            this.Property(t => t.GenId).HasColumnName("GEN_ID");

            this.Property(t => t.IconIndex).HasColumnName("ICON_INDEX");

            this.Property(t => t.Active).HasColumnName("ACTIVE");
        }
    }
}
