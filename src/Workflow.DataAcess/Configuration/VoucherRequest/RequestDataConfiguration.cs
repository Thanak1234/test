using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.VoucherRequest;

namespace Workflow.DataAcess.Configuration.VoucherRequest {
    public class RequestDataConfiguration: EntityTypeConfiguration<RequestData> {
        public RequestDataConfiguration() {
            ToTable("VR_REQUEST_DATA", "FINANCE");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.VoucherType).HasColumnName("VOUCHER_TYPE");
            Property(t => t.QtyRequest).HasColumnName("QTY_REQUEST");
            Property(t => t.DateRequired).HasColumnName("DATE_REQUIRED");
            Property(t => t.VoucherNo).HasColumnName("VOUCHER_NO");
            Property(t => t.AvailableStock).HasColumnName("AVAILABLE_STOCK");
            Property(t => t.MonthlyUtilsation).HasColumnName("MONTHLY_UTILSATION");
            Property(t => t.IsReprint).HasColumnName("IS_REPRINT");
            Property(t => t.HeaderOnVoucher).HasColumnName("HEADER_ON_VOUCHER");
            Property(t => t.Detail).HasColumnName("DETAIL");
            Property(t => t.Justification).HasColumnName("JUSTIFICATION");
            Property(t => t.ValidFrom).HasColumnName("VALID_FROM");
            Property(t => t.ValidTo).HasColumnName("VALID_TO");
            Property(t => t.Validity).HasColumnName("VALIDITY");
            Property(t => t.Discount).HasColumnName("DISCOUNT");
            Property(t => t.TC1ChangesByRequestor).HasColumnName("TC1_CHANGES_BY_REQUESTOR");
            Property(t => t.TC1ChangesByFinance).HasColumnName("TC1_CHANGES_BY_FINANCE");
            Property(t => t.TC2ChangesByRequestor).HasColumnName("TC2_CHANGES_BY_REQUESTOR");
            Property(t => t.TC2ChangesByFinance).HasColumnName("TC2_CHANGES_BY_FINANCE");
            Property(t => t.TC3ChangesByRequestor).HasColumnName("TC3_CHANGES_BY_REQUESTOR");
            Property(t => t.TC3ChangesByFinance).HasColumnName("TC3_CHANGES_BY_FINANCE");
            Property(t => t.TC4ChangesByRequestor).HasColumnName("TC4_CHANGES_BY_REQUESTOR");
            Property(t => t.TC4ChangesByFinance).HasColumnName("TC4_CHANGES_BY_FINANCE");
            Property(t => t.TC5ChangesByRequestor).HasColumnName("TC5_CHANGES_BY_REQUESTOR");
            Property(t => t.TC5ChangesByFinance).HasColumnName("TC5_CHANGES_BY_FINANCE");
            Property(t => t.TC6ChangesByRequestor).HasColumnName("TC6_CHANGES_BY_REQUESTOR");
            Property(t => t.TC6ChangesByFinance).HasColumnName("TC6_CHANGES_BY_FINANCE");
            Property(t => t.TC7ChangesByRequestor).HasColumnName("TC7_CHANGES_BY_REQUESTOR");
            Property(t => t.TC7ChangesByFinance).HasColumnName("TC7_CHANGES_BY_FINANCE");
            Property(t => t.TC8ChangesByRequestor).HasColumnName("TC8_CHANGES_BY_REQUESTOR");
            Property(t => t.TC8ChangesByFinance).HasColumnName("TC8_CHANGES_BY_FINANCE");
            Property(t => t.DoneByCreative).HasColumnName("DONE_BY_CREATIVE");
            Property(t => t.DoneByOutsideVendor).HasColumnName("DONE_BY_OUTSIDE_VENDOR");
            Property(t => t.IsHotelVoucher).HasColumnName("IS_HOTEL_VOUCHER");
            Property(t => t.IsGamingVoucher).HasColumnName("IS_GAMING_VOUCHER");
        }
    }
}
