using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;

namespace Workflow.DataAcess.Configuration
{
    public class AttachementConfiguration : AbstractModelConfiguration<FileTemp>
    {
        public AttachementConfiguration()
        {
            this.HasKey(t => t.Id);

            this.ToTable("DOCUMENTS", "BPMDATA");
            this.Property(t => t.Serial).HasColumnName("SERIAL");
            this.Property(t => t.Name).HasColumnName("NAME").HasMaxLength(100);
            this.Property(t => t.Comment).HasColumnName("COMMENT").HasMaxLength(2000);
            this.Property(t => t.FileContent).HasColumnName("UPLOADED_FILE");

            
            // Ignore Mapping
            this.Ignore(t => t.FileName);
            this.Ignore(t => t.MediaType);
            this.Ignore(t => t.FilePath);
            this.Ignore(t => t.FileBinary);
            this.Ignore(t => t.ContentDisposition);
            this.Ignore(t => t.RequestHeaderId);
        }
    }
}
