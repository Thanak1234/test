using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.N2MWO;

namespace Workflow.DataAcess.Configuration.N2MWO
{
    public class N2WorkTypeConfiguration : EntityTypeConfiguration<N2WorkType> {

        public N2WorkTypeConfiguration() {
            ToTable("MWO_WORK_TYPE", "N2_MAIN");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.Name).HasColumnName("NAME");
            Property(t => t.Sequence).HasColumnName("SEQUENCE");
        }
    }
}
