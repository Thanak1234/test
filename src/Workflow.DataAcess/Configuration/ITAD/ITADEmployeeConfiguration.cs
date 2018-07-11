using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities.ITAD;

namespace Workflow.DataAcess.Configuration.ITAD
{
    public class ITADEmployeeConfiguration : EntityTypeConfiguration<ITADEmployee>
    {
        public ITADEmployeeConfiguration()
        {
            this.HasKey(t => t.Id);
            this.ToTable("ITAD_EMPLOYEE", "IT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.Department).HasColumnName("DEPARTMENT");
            this.Property(t => t.EmployeeName).HasColumnName("DISPLAY_NAME");
            this.Property(t => t.EmployeeNo).HasColumnName("EMP_NO");
            this.Property(t => t.FirstName).HasColumnName("FIRST_NAME");
            this.Property(t => t.Position).HasColumnName("JOB_TITLE");
            this.Property(t => t.LastName).HasColumnName("LAST_NAME");
            this.Property(t => t.Mobile).HasColumnName("MOBILE");
            this.Property(t => t.NoEmail).HasColumnName("NO_EMAIL");
            this.Property(t => t.Location).HasColumnName("OFFICE_LOCATION");
            this.Property(t => t.Phone).HasColumnName("PHONE");
            this.Property(t => t.Email).HasColumnName("EMAIL");
            this.Property(t => t.Remark).HasColumnName("Remark");
        }
    }
}
