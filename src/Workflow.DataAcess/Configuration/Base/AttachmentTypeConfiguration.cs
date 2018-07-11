using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Configuration.Base
{
    public class AttachmentTypeConfiguration<T> : AbstractModelConfiguration<T> where T : FileAttachement
    {
        public AttachmentTypeConfiguration()
        {
            this.Property(t => t.Name).HasColumnName("NAME").HasMaxLength(100);
            this.Property(t => t.Comment).HasColumnName("COMMENT").HasMaxLength(2000);
            this.Property(t => t.FileContent).HasColumnName("UPLOADED_FILE");
            this.Property(t => t.Status).HasColumnName("STATUS");


            // Ignore Mapping
            this.Ignore(t => t.Serial);
            this.Ignore(t => t.FileName);
            this.Ignore(t => t.MediaType);
            this.Ignore(t => t.FilePath);
            this.Ignore(t => t.FileBinary);
            this.Ignore(t => t.ContentDisposition);
            this.Ignore(p => p.ReadOnlyRecord);

        }
    }
}
