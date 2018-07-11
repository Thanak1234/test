using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataAcess.Configuration.EOM {

    public class EOMConfiguration : EntityTypeConfiguration<Domain.Entities.EOMRequestForm.EOMDetail> {

        public EOMConfiguration() {
            ToTable("EOM", "HR");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.Month).HasColumnName("MONTH");
            Property(t => t.Aprd).HasColumnName("APRD");
            Property(t => t.Cfie).HasColumnName("CFIE");
            Property(t => t.Lc).HasColumnName("LC");
            Property(t => t.Tmp).HasColumnName("TMP");
            Property(t => t.Psdm).HasColumnName("PSDM");
            Property(t => t.TotalScore).HasColumnName("TOTAL_SCORE");
            Property(t => t.Reason).HasColumnName("REASON");
            Property(t => t.Cash).HasColumnName("CASH");
            Property(t => t.Voucher).HasColumnName("VOUCHER");
        }
    }
}
