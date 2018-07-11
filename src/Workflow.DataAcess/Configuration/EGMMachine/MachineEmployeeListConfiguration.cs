using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.EGM;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration.EGMMachine
{
    public class MachineEmployeeListConfiguration : EntityTypeConfiguration<MachineEmployee>
    {

        public MachineEmployeeListConfiguration()
        {
            //Primary Key
            this.HasKey(t => t.id);

            //Table & Column Mapping
            this.ToTable("MACHINE_EMPLOYEELIST", "EGM");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.empno).HasColumnName("EMPNO");
            this.Property(t => t.request_header_id).HasColumnName("REQUEST_HEADER_ID");
        }

    }
}
