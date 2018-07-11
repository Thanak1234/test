


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class DeptAccessRightConfiguration : EntityTypeConfiguration<DeptAccessRight>
    {
        public DeptAccessRightConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.ReqApp)
                .IsRequired()
                .HasMaxLength(10);


            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);


            this.Property(t => t.ModifiedBy)
                .HasMaxLength(50);


            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(10);


            // Table & Column Configurationpings

            this.ToTable("DEPT_ACCESS_RIGHT", "BPMDATA");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.DeptId).HasColumnName("DEPT_ID");

            this.Property(t => t.UserId).HasColumnName("USER_ID");

            this.Property(t => t.ReqApp).HasColumnName("REQ_APP");

            this.Property(t => t.CreatedBy).HasColumnName("CREATED_BY");

            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");

            this.Property(t => t.ModifiedBy).HasColumnName("MODIFIED_BY");

            this.Property(t => t.ModifiedDate).HasColumnName("MODIFIED_DATE");

            this.Property(t => t.Sync).HasColumnName("SYNC");

            this.Property(t => t.Status).HasColumnName("STATUS");

        }
    }
}
