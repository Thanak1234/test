using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Attachment;

namespace Workflow.DataAcess.Configuration.Attachment
{

    public class RequisitionConfiguration : AttachmentTypeConfiguration<Requisition> {
        public RequisitionConfiguration()
        {
            this.ToTable("EMPLOYEE_REQUISITION_FILES", "HR");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
        }
    }
}
