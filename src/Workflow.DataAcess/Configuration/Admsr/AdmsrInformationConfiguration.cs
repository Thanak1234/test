using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities.Admsr;

namespace Workflow.DataAcess.Configuration.Admsr
{

    public class AdmsrInformationConfiguration: EntityTypeConfiguration<AdmsrInformation> {
        public AdmsrInformationConfiguration() {
            HasKey(t => t.Id);
            ToTable("ADMSR_INFORMATION", "HR");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.Adc).HasColumnName("ADC");
            Property(t => t.Dr).HasColumnName("DR");
            Property(t => t.Dsrj).HasColumnName("DSRJ");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.Salod).HasColumnName("SALOD");
            Property(t => t.Slod).HasColumnName("SLOD");
            Property(t => t.ECC).HasColumnName("E_CC");
            Property(t => t.Efinance).HasColumnName("E_FINANCE");
            Property(t => t.Epurchasing).HasColumnName("E_PURCHASING");
        }
    }

}
