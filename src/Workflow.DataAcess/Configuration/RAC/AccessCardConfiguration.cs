using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities.RAC;

namespace Workflow.DataAcess.Configuration.RAC {

    public class AccessCardConfiguration : EntityTypeConfiguration<AccessCard> {
        public AccessCardConfiguration() {
            HasKey(t => t.Id);
            ToTable("ACCESS_CARD", "SERVEILLANCE");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.Item).HasColumnName("ITEM");
            Property(t => t.Reason).HasColumnName("REASON");
            Property(t => t.Remark).HasColumnName("REMARK");
            Property(t => t.IssueDate).HasColumnName("DATE_ISSUE");
            Property(t => t.SerialNo).HasColumnName("SERIAL_NO");
        }
    }

}
