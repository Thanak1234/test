using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Attachment;

namespace Workflow.DataAcess.Configuration.Attachment
{

    public class ComplimentaryConfiguration : AttachmentTypeConfiguration<Complimentary> {
        public ComplimentaryConfiguration()
        {
            this.ToTable("COMPLIMENTARY_ROOM_FILES", "RESERVATION");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
        }
    }
}
