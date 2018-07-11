using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.EGM;

namespace Workflow.DataAcess.Configuration.EGMAttandance
{
    public class AttandanceAttachmentConfiguration : AttachmentTypeConfiguration<AttandanceAttachment>
    {
        public AttandanceAttachmentConfiguration()
        {
            this.ToTable("ATD_REQUEST_FILES", "EGM");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
        }
    }
}
