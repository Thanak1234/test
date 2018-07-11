using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities.Admsr;

namespace Workflow.DataAcess.Configuration.Admsr
{
    public class AdmsrCompnayConfiguration : EntityTypeConfiguration<AdmsrCompany> {
        public AdmsrCompnayConfiguration() {
            HasKey(t => t.Id);
            ToTable("ADMSR_COMPANY", "HR");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.DateIssued).HasColumnName("DATE_ISSUED");
            Property(t => t.Name).HasColumnName("NAME");
            Property(t => t.Price).HasColumnName("PRICE");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.ValidDay).HasColumnName("VALID_DAY");
        }
    }
}
