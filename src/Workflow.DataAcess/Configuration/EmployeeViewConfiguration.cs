using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Core;

namespace Workflow.DataAcess.Configuration {

    public class EmployeeViewConfiguration : EntityTypeConfiguration<EmployeeView> {

        public EmployeeViewConfiguration() {

            this.ToTable("VIEW_WF_EMPLOYEE", "HR");

            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.LoginName).HasColumnName("LOGIN_NAME");
            this.Property(t => t.EmpNo).HasColumnName("EMP_NO");
            this.Property(t => t.DisplayName).HasColumnName("DISPLAY_NAME");
            this.Property(t => t.Position).HasColumnName("POSITION");
            this.Property(t => t.Email).HasColumnName("EMAIL");
            this.Property(t => t.Telephone).HasColumnName("TELEPHONE");
            this.Property(t => t.MobilePhone).HasColumnName("MOBILE_PHONE");
            this.Property(t => t.Manager).HasColumnName("MANAGER");
            this.Property(t => t.Hod).HasColumnName("HOD");
            this.Property(t => t.HodName).HasColumnName("HODName");
            this.Property(t => t.GroupName).HasColumnName("GROUP_NAME");
            this.Property(t => t.DeptName).HasColumnName("DEPT_NAME");
            this.Property(t => t.TeamName).HasColumnName("TEAM_NAME");
            this.Property(t => t.DeptType).HasColumnName("DEPT_TYPE");
            this.Property(t => t.DeptId).HasColumnName("DEPT_ID");
            this.Property(t => t.TeamId).HasColumnName("TEAM_ID");
            this.Property(t => t.EmpType).HasColumnName("EMP_TYPE");
        }

    }
}
