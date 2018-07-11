using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.VoucherRequest {
    public class RequestData {
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public string VoucherType { get; set; }
        public int? QtyRequest { get; set; }
        public DateTime? DateRequired { get; set; }
        public string VoucherNo { get; set; }
        public int? AvailableStock { get; set; }
        public int? MonthlyUtilsation { get; set; }
        public bool? IsReprint { get; set; }
        public string HeaderOnVoucher { get; set; }
        public string Detail { get; set; }
        public string Justification { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string Validity { get; set; }
        public decimal? Discount { get; set; }
        public string TC1ChangesByRequestor { get; set; }
        public string TC1ChangesByFinance { get; set; }
        public string TC2ChangesByRequestor { get; set; }
        public string TC2ChangesByFinance { get; set; }
        public string TC3ChangesByRequestor { get; set; }
        public string TC3ChangesByFinance { get; set; }
        public string TC4ChangesByRequestor { get; set; }
        public string TC4ChangesByFinance { get; set; }
        public string TC5ChangesByRequestor { get; set; }
        public string TC5ChangesByFinance { get; set; }
        public string TC6ChangesByRequestor { get; set; }
        public string TC6ChangesByFinance { get; set; }
        public string TC7ChangesByRequestor { get; set; }
        public string TC7ChangesByFinance { get; set; }
        public string TC8ChangesByRequestor { get; set; }
        public string TC8ChangesByFinance { get; set; }
        public bool? DoneByCreative { get; set; }
        public bool? DoneByOutsideVendor { get; set; }
        public bool? IsHotelVoucher { get; set; }
        public bool? IsGamingVoucher { get; set; }
    }
}
