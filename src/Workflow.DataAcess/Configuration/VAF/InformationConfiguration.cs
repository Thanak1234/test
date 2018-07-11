using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.VAF;

namespace Workflow.DataAcess.Configuration.VAF {

    public class InformationConfiguration: EntityTypeConfiguration<Information> {
        public InformationConfiguration() {
            HasKey(t => t.Id);
            ToTable("VAF_INFO", "FINANCE");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.AdjType).HasColumnName("ADJ_TYPE");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.Remark).HasColumnName("REMARK");
        }
    }

}
