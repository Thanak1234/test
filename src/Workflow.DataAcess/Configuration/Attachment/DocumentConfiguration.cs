using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Attachment;

namespace Workflow.DataAcess.Configuration.Attachment
{

    public class DocumentConfiguration : AttachmentTypeConfiguration<Document> {
        public DocumentConfiguration()
        {
            this.ToTable("ATTACHMENT_FILES", "BPMDATA");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
        }
    }
}
