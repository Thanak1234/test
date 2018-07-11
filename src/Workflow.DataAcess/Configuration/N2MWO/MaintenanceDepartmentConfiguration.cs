using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.N2MWO;

namespace Workflow.DataAcess.Configuration.N2MWO
{
    public class N2MaintenanceDepartmentConfiguration : EntityTypeConfiguration<N2MaintenanceDepartment> {

        public N2MaintenanceDepartmentConfiguration() {
            ToTable("MWO_MAINTENANCE_DEPARTMENT", "N2_MAIN");
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
