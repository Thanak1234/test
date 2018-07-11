using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.INCIDENT;

namespace Workflow.DataAcess.Configuration.Incident
{
    public class EmployeeListConfiguration : EntityTypeConfiguration<IncidentEmployee>
    {
        public EmployeeListConfiguration()
        {
            //Primary Key
            this.HasKey(t => t.id);

            //Table & Column Mapping
            this.ToTable("INCIDENT_EMPLOYEELIST", "EGM");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.employeeno).HasColumnName("EMPNO");
            this.Property(t => t.requestheaderid).HasColumnName("REQUEST_HEADER_ID");
        }
    }
}
