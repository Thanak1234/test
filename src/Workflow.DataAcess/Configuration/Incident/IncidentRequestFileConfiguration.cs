using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.INCIDENT;

namespace Workflow.DataAcess.Configuration.Incident
{
    public class IncidentRequestFileConfiguration : AttachmentTypeConfiguration<IncidentAttachement>
    {
        public IncidentRequestFileConfiguration()
        {
            this.ToTable("ICD_REQUEST_FILES", "EGM");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");            
        }
    }
}
