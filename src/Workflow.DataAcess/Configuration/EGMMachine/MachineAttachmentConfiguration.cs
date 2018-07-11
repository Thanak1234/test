using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.EGM;

namespace Workflow.DataAcess.Configuration.EGMMachine
{
    public class MachineAttachmentConfiguration : AttachmentTypeConfiguration<MachineAttachment>
    {
        public MachineAttachmentConfiguration()
        {
            this.ToTable("MCN_REQUEST_FILES", "EGM");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
        }
    }
}
