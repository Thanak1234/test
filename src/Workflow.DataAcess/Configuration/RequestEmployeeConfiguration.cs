


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class RequestEmployeeConfiguration : EntityTypeConfiguration<RequestEmployee>
    {
        public RequestEmployeeConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.LoginName)
                .IsRequired()
                .HasMaxLength(255);


            this.Property(t => t.EmpNo)
                .HasMaxLength(255);


            this.Property(t => t.JobTitle)
                .HasMaxLength(255);


            this.Property(t => t.FirstName)
                .HasMaxLength(255);


            this.Property(t => t.LastName)
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


            // Table & Column Configurationpings

            this.ToTable("REQUEST_EMPLOYEE", "BPMDATA");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");

            this.Property(t => t.LoginName).HasColumnName("LOGIN_NAME");

            this.Property(t => t.EmpNo).HasColumnName("EMP_NO");

            this.Property(t => t.JobTitle).HasColumnName("JOB_TITLE");

            this.Property(t => t.FirstName).HasColumnName("FIRST_NAME");

            this.Property(t => t.LastName).HasColumnName("LAST_NAME");

            this.Property(t => t.DisplayName).HasColumnName("DISPLAY_NAME");

            this.Property(t => t.Email).HasColumnName("EMAIL");

            this.Property(t => t.DeptId).HasColumnName("DEPT_ID");

            this.Property(t => t.Telephone).HasColumnName("TELEPHONE");

            this.Property(t => t.MobilePhone).HasColumnName("MOBILE_PHONE");

            this.Property(t => t.HomePhone).HasColumnName("HOME_PHONE");

            this.Property(t => t.IpPhone).HasColumnName("IP_PHONE");

            this.Property(t => t.Address).HasColumnName("ADDRESS");

            this.Property(t => t.HiredDate).HasColumnName("HIRED_DATE");

            this.Property(t => t.ReportTo).HasColumnName("REPORT_TO");

            this.Property(t => t.DeptName).HasColumnName("DEPT_NAME");

        }
    }
}
