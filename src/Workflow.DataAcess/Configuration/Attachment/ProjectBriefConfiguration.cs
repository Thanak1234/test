using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Attachment;

namespace Workflow.DataAcess.Configuration.Attachment
{

    public class ProjectBriefConfiguration : AttachmentTypeConfiguration<ProjectBrief> {
        public ProjectBriefConfiguration()
        {
            this.ToTable("PROJECT_BRIEF_FILES", "EVENT");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
        }
    }
}
