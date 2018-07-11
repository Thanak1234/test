using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.EOMRequestForm;
using Workflow.Domain.Entities.MWO;

namespace Workflow.DataAcess.Configuration.EOM {
    public class EOMUploadFileConfiguration: AttachmentTypeConfiguration<EOMUploadFile> {

        public EOMUploadFileConfiguration() {
            ToTable("EOM_REQUEST_FILES", "HR");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
        }
    }
}
