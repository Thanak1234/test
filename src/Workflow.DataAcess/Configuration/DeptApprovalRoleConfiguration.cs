


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class DeptApprovalRoleConfiguration : EntityTypeConfiguration<DeptApprovalRole>
    {
        public DeptApprovalRoleConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.RequestCode)
                .IsRequired()
                .HasMaxLength(50);


            this.Property(t => t.RoleCode)
                .HasMaxLength(20);


            this.Property(t => t.DeptApprovalRole1)
                .IsRequired()
                .HasMaxLength(50);


            this.Property(t => t.Description)
                .HasMaxLength(150);


            // Table & Column Configurationpings

            this.ToTable("DEPT_APPROVAL_ROLE", "BPMDATA");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.RequestCode).HasColumnName("REQUEST_CODE");

            this.Property(t => t.RoleCode).HasColumnName("ROLE_CODE");

            this.Property(t => t.ActId).HasColumnName("ACT_ID");

            this.Property(t => t.DeptId).HasColumnName("DEPT_ID");

            this.Property(t => t.DeptApprovalRole1).HasColumnName("DEPT_APPROVAL_ROLE");

            this.Property(t => t.RoleGuid).HasColumnName("ROLE_GUID");

            this.Property(t => t.Description).HasColumnName("DESCRIPTION");

            this.Property(t => t.Active).HasColumnName("ACTIVE");

        }
    }
}
