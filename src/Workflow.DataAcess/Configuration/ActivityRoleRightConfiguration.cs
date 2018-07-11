


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class ActivityRoleRightConfiguration : EntityTypeConfiguration<ActivityRoleRight>
    {
        public ActivityRoleRightConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.Description)
                .HasMaxLength(100);


            this.Property(t => t.CreatedBy)
                .HasMaxLength(100);


            this.Property(t => t.ModifiedBy)
                .HasMaxLength(100);


            // Table & Column Configurationpings

            this.ToTable("ACTIVITY_ROLE_RIGHT", "ADMIN");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.EmployeeId).HasColumnName("EMPLOYEE_ID");

            this.Property(t => t.DeptApprovalRoleId).HasColumnName("DEPT_APPROVAL_ROLE_ID");

            this.Property(t => t.Description).HasColumnName("DESCRIPTION");

            this.Property(t => t.Active).HasColumnName("ACTIVE");

            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");

            this.Property(t => t.ModifiedDate).HasColumnName("MODIFIED_DATE");

            this.Property(t => t.CreatedBy).HasColumnName("CREATED_BY");

            this.Property(t => t.ModifiedBy).HasColumnName("MODIFIED_BY");

        }
    }
}
