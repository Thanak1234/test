using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.IRF;

namespace Workflow.DataAcess.Configuration.IRF {
    public class IRFRequestItemConfiguration : EntityTypeConfiguration<IRFRequestItem> {
        public IRFRequestItemConfiguration() {
            HasKey(t => t.Id);
            ToTable("ITIRF_REQUEST_ITEM", "IT");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.ItemModel).HasColumnName("ITEM_MODEL");
            Property(t => t.ItemName).HasColumnName("ITEM_NAME");
            Property(t => t.PartNo).HasColumnName("PART_NO");
            Property(t => t.Qty).HasColumnName("QTY");
            Property(t => t.Remark).HasColumnName("REMARK");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.ReturnDate).HasColumnName("RETURN_DATE");
            Property(t => t.SendDate).HasColumnName("SEND_DATE");
            Property(t => t.SerialNo).HasColumnName("SERIAL_NO");
        }
    }
}
