using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.MWO;

namespace Workflow.DataAcess.Configuration.MWO {
    public class DepartmentChargableConfiguration : EntityTypeConfiguration<DepartmentChargable> {

        public DepartmentChargableConfiguration() {
            ToTable("MWO_DEPARTMENT_CHARGABLE", "MAINTENANCE");
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
