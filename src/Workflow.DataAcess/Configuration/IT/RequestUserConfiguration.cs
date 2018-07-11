using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Configuration
{
    public class RequestUserConfiguration : EntityTypeConfiguration<RequestUser>
    {
        public RequestUserConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);

            // Table & Column Mappings

            this.ToTable("REQUEST_USER", "IT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.EmpId).HasColumnName("EMP_ID");
            this.Property(t => t.EmpName).HasColumnName("EMP_NAME");
            this.Property(t => t.EmpNo).HasColumnName("EMP_NO");
            this.Property(t => t.TeamId).HasColumnName("DEPT_ID");
            this.Property(t => t.Position).HasColumnName("POSITION");
            this.Property(t => t.Email).HasColumnName("EMAIL");
            this.Property(t => t.HiredDate).HasColumnName("HIRED_DATE");
            this.Property(t => t.Manager).HasColumnName("MANAGER");
            this.Property(t => t.Phone).HasColumnName("PHONE");
            this.Property(t => t.Version).HasColumnName("VERSION");
            this.HasRequired<Department>(p => p.Team).WithMany().HasForeignKey(p => p.TeamId);
            this.HasRequired<RequestHeader>(p => p.RequestHeader).WithMany().HasForeignKey(p => p.RequestHeaderId);
        }

    }
}
