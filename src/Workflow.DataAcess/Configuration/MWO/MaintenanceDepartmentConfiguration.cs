using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.MWO;

namespace Workflow.DataAcess.Configuration.MWO {
    public class MaintenanceDepartmentConfiguration : EntityTypeConfiguration<MaintenanceDepartment> {

        public MaintenanceDepartmentConfiguration() {
            ToTable("MWO_MAINTENANCE_DEPARTMENT", "MAINTENANCE");
            HasKey(t => t.Id);

            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.Instruction).HasColumnName("INSTRUCTION");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.TechnicianId).HasColumnName("TECHNICIAN_ID");
            Property(t => t.WorkTypeId).HasColumnName("WORK_TYPE_ID");
            Property(t => t.AssignDate).HasColumnName("ASSIGN_DATE");
        }
    }
}
