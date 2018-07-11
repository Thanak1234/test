using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.AV;

namespace Workflow.DataAcess.Configuration.AV {

    public class AVJBRequestFilesConfiguration: AttachmentTypeConfiguration<AvbUploadFile> {
        public AVJBRequestFilesConfiguration()
        {
            this.ToTable("AVJB_REQUEST_FILES", "EVENT");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");

        }
    }
}
