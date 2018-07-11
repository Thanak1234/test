using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities.Core.CCR;

namespace Workflow.DataAcess.Configuration.CCR {
    public class ContractDraftConfiguration : EntityTypeConfiguration<ContractDraft> {
        public ContractDraftConfiguration() {
            ToTable("CONTRACT_DRAFT", "LEGAL");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.Name).HasColumnName("NAME");
            Property(t => t.Vat).HasColumnName("VAT");
            Property(t => t.Address).HasColumnName("ADDRESS");
            Property(t => t.Email).HasColumnName("EMAIL");
            Property(t => t.RegistrationNo).HasColumnName("REGISTRATION_NO");
            Property(t => t.Phone).HasColumnName("PHONE");
            Property(t => t.ContactName).HasColumnName("CONTACT_NAME");
            Property(t => t.Position).HasColumnName("POSITION");
            Property(t => t.IssueedBy).HasColumnName("ISSUEED_BY");
            Property(t => t.Term).HasColumnName("TERM");
            Property(t => t.StartDate).HasColumnName("START_DATE");
            Property(t => t.InclusiveTax).HasColumnName("INCLUSIVE_TAX");
            Property(t => t.EndingDate).HasColumnName("ENDING_DATE");
            Property(t => t.PaymentTerm).HasColumnName("PAYMENT_TERM");
            Property(t => t.AtSa).HasColumnName("AT_SA");
            Property(t => t.AtSpa).HasColumnName("AT_SPA");
            Property(t => t.AtLa).HasColumnName("AT_LA");
            Property(t => t.AtCa).HasColumnName("AT_CA");
            Property(t => t.AtLea).HasColumnName("AT_LEA");
            Property(t => t.AtEa).HasColumnName("AT_EA");
            Property(t => t.AtOther).HasColumnName("AT_OTHER");
            Property(t => t.Vis).HasColumnName("VIS");
            Property(t => t.StatusNew).HasColumnName("STATUS_NEW");
            Property(t => t.StatusRenewal).HasColumnName("STATUS_RENEWAL");
            Property(t => t.StatusReplacement).HasColumnName("STATUS_REPLACEMENT");
            Property(t => t.StatusAddendum).HasColumnName("STATUS_ADDENDUM");
            Property(t => t.IsCapex).HasColumnName("IS_CAPEX");
            Property(t => t.BcjNumber).HasColumnName("BCJ_NUMBER");
            Property(t => t.ActA).HasColumnName("ACT_A");
            Property(t => t.ActB).HasColumnName("ACT_B");
            Property(t => t.ActC).HasColumnName("ACT_C");
            Property(t => t.ActD).HasColumnName("ACT_D");
        }
    }
}
