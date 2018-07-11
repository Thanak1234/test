using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities.Queue;

namespace Workflow.DataAcess.Configuration.Queue {
    public class FingerPrintConfiguration : EntityTypeConfiguration<FingerPrint>
    {
        public FingerPrintConfiguration()
        {
            this.HasKey(t => t.Id);
            this.ToTable("FINGER_PRINT", "QUEUE");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.AttState).HasColumnName("ATT_STATE");
            this.Property(t => t.EnrolmentNo).HasColumnName("ENROLMENT_NO");
            this.Property(t => t.IP).HasColumnName("IP");
            this.Property(t => t.IsInvalid).HasColumnName("IS_INVALID");
            this.Property(t => t.MachineDate).HasColumnName("MACHINE_DATE");
            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            this.Property(t => t.Port).HasColumnName("PORT");
            this.Property(t => t.Status).HasColumnName("STATUS");
            this.Property(t => t.VerifyMethod).HasColumnName("VERIFY_METHOD");
            this.Property(t => t.WorkCode).HasColumnName("WORK_CODE");
            this.Property(t => t.MachineNo).HasColumnName("MACHINE_NO");
        }
    }
}
