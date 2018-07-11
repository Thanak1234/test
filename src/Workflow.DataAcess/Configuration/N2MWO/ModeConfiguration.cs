using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.N2MWO;

namespace Workflow.DataAcess.Configuration.N2MWO
{
    public class N2ModeConfiguration : EntityTypeConfiguration<N2Mode> {

        public N2ModeConfiguration() {
            ToTable("MWO_MODE", "N2_MAIN");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.Name).HasColumnName("NAME");
            Property(t => t.Sequence).HasColumnName("SEQUENCE");
        }
    }
}
