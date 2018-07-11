using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Configuration
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {

        public EmployeeConfiguration() {


            // Primary Key

            this.HasKey(t => t.Id);

            this.Property(t => t.LoginName)
                .HasMaxLength(255);


            this.Property(t => t.EmpNo)
                .HasMaxLength(255);


            this.Property(t => t.JobTitle)
                .HasMaxLength(255);


            this.Property(t => t.DisplayName)
                .HasMaxLength(255);


            this.Property(t => t.Email)
                .HasMaxLength(255);


            this.Property(t => t.Telephone)
                .HasMaxLength(255);


            this.Property(t => t.MobilePhone)
                .HasMaxLength(255);


            this.Property(t => t.HomePhone)
                .HasMaxLength(255);


            this.Property(t => t.IpPhone)
                .HasMaxLength(255);


            this.Property(t => t.Address)
                .HasMaxLength(255);


            this.Property(t => t.ReportTo)
                .HasMaxLength(255);


            this.Property(t => t.DeptName)
                .HasMaxLength(255);


            this.Property(t => t.FirstName)
                .HasMaxLength(255);


            this.Property(t => t.LastName)
                .HasMaxLength(255);


            this.Property(t => t.EmpType)
                .HasMaxLength(50);


            // Table & Column Mappings

            this.ToTable("EMPLOYEE", "HR");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.DeptId).HasColumnName("DEPT_ID");

            this.Property(t => t.LoginName).HasColumnName("LOGIN_NAME");

            this.Property(t => t.EmpNo).HasColumnName("EMP_NO");

            this.Property(t => t.JobTitle).HasColumnName("JOB_TITLE");

            this.Property(t => t.DisplayName).HasColumnName("DISPLAY_NAME");

            this.Property(t => t.Email).HasColumnName("EMAIL");

            this.Property(t => t.Telephone).HasColumnName("TELEPHONE");

            this.Property(t => t.MobilePhone).HasColumnName("MOBILE_PHONE");

            this.Property(t => t.HomePhone).HasColumnName("HOME_PHONE");

            this.Property(t => t.IpPhone).HasColumnName("IP_PHONE");

            this.Property(t => t.Address).HasColumnName("ADDRESS");

            this.Property(t => t.HiredDate).HasColumnName("HIRED_DATE");

            this.Property(t => t.ReportTo).HasColumnName("REPORT_TO");

            this.Property(t => t.DeptName).HasColumnName("DEPT_NAME");

            this.Property(t => t.FirstName).HasColumnName("FIRST_NAME");

            this.Property(t => t.LastName).HasColumnName("LAST_NAME");

            this.Property(t => t.EmpType).HasColumnName("EMP_TYPE");

            this.Property(t => t.Active).HasColumnName("ACTIVE");

            this.HasRequired<Department>(p => p.Department).WithMany().HasForeignKey(p => p.DeptId);
        }


    }
}
