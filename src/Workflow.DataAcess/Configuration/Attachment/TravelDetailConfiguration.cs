using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Attachment;

namespace Workflow.DataAcess.Configuration.Attachment
{

    public class TravelDetailConfiguration : AttachmentTypeConfiguration<TravelDetail> {
        public TravelDetailConfiguration()
        {
            this.ToTable("ATT_REQUEST_FILES", "HR");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
        }
    }
}
