using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Configuration
{
    public class ItRequestFilesConfiguration : AttachmentTypeConfiguration<ItRequestFiles> 
    {
        public ItRequestFilesConfiguration()
        {
            this.ToTable("IT_REQUEST_FILES", "IT");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
        }
    }
}
