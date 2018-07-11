using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.N2MWO;

namespace Workflow.DataAcess.Configuration.N2MWO
{
    public class N2DepartmentChargableConfiguration : EntityTypeConfiguration<N2DepartmentChargable> {

        public N2DepartmentChargableConfiguration() {
            ToTable("MWO_DEPARTMENT_CHARGABLE", "N2_MAIN");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.Location).HasColumnName("LOCATION");
            Property(t => t.CCD).HasColumnName("CCD");
            Property(t => t.Department).HasColumnName("DEPARTMENT");
            Property(t => t.Sequence).HasColumnName("SEQUENCE");
            Property(t => t.CurrentNumber).HasColumnName("CURRENT_NUMBER");
        }
    }
}
