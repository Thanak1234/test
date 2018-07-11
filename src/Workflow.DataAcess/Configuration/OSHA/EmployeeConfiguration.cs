using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataAcess.Configuration.OSHA {
    public class EmployeeConfiguration : EntityTypeConfiguration<Domain.Entities.OSHA.OSHAEmployee> {

        public EmployeeConfiguration() {
            ToTable("EMPLOYEES", "OSHA");
            HasKey(t => t.Id);
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.DeptName).HasColumnName("DEPT_NAME");
            Property(t => t.Email).HasColumnName("EMAIL");
            Property(t => t.EmpName).HasColumnName("EMP_NAME");
            Property(t => t.EmpNo).HasColumnName("EMP_NO");
            Property(t => t.EmpType).HasColumnName("EMP_TYPE");
            Property(t => t.Phone).HasColumnName("PHONE");
            Property(t => t.Position).HasColumnName("POSITION");
        }
    }
}
