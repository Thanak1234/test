using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.BCJ;

namespace Workflow.DataAcess.Configuration.BCJ
{

    public class AttachmentConfiguration : AttachmentTypeConfiguration<BcjAttachment> {
        public AttachmentConfiguration()
        {
            this.ToTable("BCJ_REQUEST_FILES", "FINANCE");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
        }
    }
}
