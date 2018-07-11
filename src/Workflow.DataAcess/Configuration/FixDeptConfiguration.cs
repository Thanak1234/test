


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class FixDeptConfiguration : EntityTypeConfiguration<FixDept>
    {
        public FixDeptConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.EmpId);


            // Properties

            this.Property(t => t.EmpId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


            // Table & Column Configurationpings

            this.ToTable("FIX_DEPT", "DATAFIX");

            this.Property(t => t.EmpId).HasColumnName("EMP_ID");

            this.Property(t => t.CurDeptId).HasColumnName("CUR_DEPT_ID");

            this.Property(t => t.CorrectDeptId).HasColumnName("CORRECT_DEPT_ID");

            this.Property(t => t.Active).HasColumnName("ACTIVE");

        }
    }
}
