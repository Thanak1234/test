using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities.Queue;

namespace Workflow.DataAcess.Configuration.Queue {
    public class FingerPrintMachineConfiguration : EntityTypeConfiguration<FingerPrintMachine>
    {
        public FingerPrintMachineConfiguration()
        {
            this.HasKey(t => t.Id);
            this.ToTable("FINGER_PRINT_MACHINE", "QUEUE");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.IP).HasColumnName("IP");
            this.Property(t => t.Port).HasColumnName("PORT");
            this.Property(t => t.MachineNo).HasColumnName("MACHINE_NO");
            this.Property(t => t.ConnectedDate).HasColumnName("CONNECTED_DATE");
            this.Property(t => t.LastConnectedDate).HasColumnName("LAST_CONNECTED_DATE");
            this.Property(t => t.Remark).HasColumnName("REMARK");
            this.Property(t => t.Status).HasColumnName("STATUS");
            this.Property(t => t.Active).HasColumnName("ACTIVE");
            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}
