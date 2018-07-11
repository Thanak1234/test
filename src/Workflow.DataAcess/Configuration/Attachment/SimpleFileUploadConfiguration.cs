using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Attachment;

namespace Workflow.DataAcess.Configuration.Attachment
{
    public class SimpleFileUploadConfiguration : EntityTypeConfiguration<UploadFile>
    {
        public SimpleFileUploadConfiguration() {
            
            this.HasKey(t => t.Id);

            this.ToTable("UPLOAD_FILE", "BPMDATA");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.Serial).HasColumnName("SERIAL");
            this.Property(t => t.DataContent).HasColumnName("DATA_CONTENT");
            this.Property(t => t.UploadedDate).HasColumnName("UPLOADED_DATE");
            this.Property(t => t.Status).HasColumnName("STATUS");
        }
    }
}
