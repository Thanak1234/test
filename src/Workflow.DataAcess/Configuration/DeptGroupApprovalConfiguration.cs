


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class DeptGroupApprovalConfiguration : EntityTypeConfiguration<DeptGroupApproval>
    {
        public DeptGroupApprovalConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.DeptName);


            // Properties

            this.Property(t => t.RequestCode)
                .HasMaxLength(50);


            this.Property(t => t.User)
                .HasMaxLength(447);


            this.Property(t => t.DpetCode)
                .HasMaxLength(50);


            this.Property(t => t.DeptName)
                .IsRequired()
                .HasMaxLength(100);


            // Table & Column Configurationpings

            this.ToTable("DEPT_GROUP_APPROVAL");

            this.Property(t => t.RequestCode).HasColumnName("REQUEST_CODE");

            this.Property(t => t.User).HasColumnName("USER");

            this.Property(t => t.DpetCode).HasColumnName("DPET_CODE");

            this.Property(t => t.DeptName).HasColumnName("DEPT_NAME");

        }
    }
}
