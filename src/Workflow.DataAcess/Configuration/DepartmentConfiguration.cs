using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Configuration
{
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);

            // Table & Column Mappings

           

            // Table & Column Mappings

            this.ToTable("DEPARTMENT", "HR");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.MainId).HasColumnName("MAIN_ID");
            this.Property(t => t.Deptname).HasColumnName("DEPT_NAME");
            this.Property(t => t.DeptCode).HasColumnName("DPET_CODE");
            this.Property(t => t.DeptType).HasColumnName("DEPT_TYPE");
            this.Property(t => t.HoD).HasColumnName("HOD_ID");
            this.Property(t => t.Status).HasColumnName("STATUS");

        }
    }
}
