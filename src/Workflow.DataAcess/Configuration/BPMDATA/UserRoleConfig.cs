using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Core;

namespace Workflow.DataAcess.Configuration.BPMDATA {
    public class UserRoleConfig: EntityTypeConfiguration<BpmUserRole>
    {

        public UserRoleConfig() {
            this.HasKey(p => p.Id);

            ToTable("USER_ROLE", "BPMDATA");
            Property(p => p.Id).HasColumnName("ID");
            Property(p => p.EmpId).HasColumnName("EMP_ID");
            Property(p => p.RoleCode).HasColumnName("ROLE_CODE");
            Property(p => p.Status).HasColumnName("STATUS");
        }

    }
}
