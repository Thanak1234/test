using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    public class VRProcInst : ProcInst
    {
        [DataMember(Name = "voucherType")]
        public string VOUCHER_TYPE { get; set; }
        [DataMember(Name = "qtyRequest")]
        public int? QTY_REQUEST { get; set; }
        [DataMember(Name = "dateRequired")]
        public DateTime? DATE_REQUIRED { get; set; }
        [DataMember(Name = "voucherNo")]
        public string VOUCHER_NO { get; set; }
        [DataMember(Name = "availableStock")]
        public int? AVAILABLE_STOCK { get; set; }
        [DataMember(Name = "monthlyUtilsation")]
        public int? MONTHLY_UTILSATION { get; set; }
        [DataMember(Name = "isReprint")]
        public bool? IS_REPRINT { get; set; }
        [DataMember(Name = "headerOnVoucher")]
        public string HEADER_ON_VOUCHER { get; set; }
        [DataMember(Name = "detail")]
        public string DETAIL { get; set; }
        [DataMember(Name = "justification")]
        public string JUSTIFICATION { get; set; }
        [DataMember(Name = "validFrom")]
        public DateTime? VALID_FROM { get; set; }
        [DataMember(Name = "validTo")]
        public DateTime? VALID_TO { get; set; }
        [DataMember(Name = "validity")]
        public string VALIDITY { get; set; }
        [DataMember(Name = "discount")]
        public decimal? DISCOUNT { get; set; }
        [DataMember(Name = "tc1ChangesByRequestor")]
        public string TC1_CHANGES_BY_REQUESTOR { get; set; }
        [DataMember(Name = "tc1ChangesByFinance")]
        public string TC1_CHANGES_BY_FINANCE { get; set; }
        [DataMember(Name = "tc2ChangesByRequestor")]
        public string TC2_CHANGES_BY_REQUESTOR { get; set; }
        [DataMember(Name = "tc2ChangesByFinance")]
        public string TC2_CHANGES_BY_FINANCE { get; set; }
        [DataMember(Name = "tc3ChangesByRequestor")]
        public string TC3_CHANGES_BY_REQUESTOR { get; set; }
        [DataMember(Name = "tc3ChangesByFinance")]
        public string TC3_CHANGES_BY_FINANCE { get; set; }
        [DataMember(Name = "tc4ChangesByRequestor")]
        public string TC4_CHANGES_BY_REQUESTOR { get; set; }
        [DataMember(Name = "tc4ChangesByFinance")]
        public string TC4_CHANGES_BY_FINANCE { get; set; }
        [DataMember(Name = "tc5ChangesByRequestor")]
        public string TC5_CHANGES_BY_REQUESTOR { get; set; }
        [DataMember(Name = "tc5ChangesByFinance")]
        public string TC5_CHANGES_BY_FINANCE { get; set; }
        [DataMember(Name = "tc6ChangesByRequestor")]
        public string TC6_CHANGES_BY_REQUESTOR { get; set; }
        [DataMember(Name = "tc6ChangesByFinance")]
        public string TC6_CHANGES_BY_FINANCE { get; set; }
        [DataMember(Name = "tc7ChangesByRequestor")]
        public string TC7_CHANGES_BY_REQUESTOR { get; set; }
        [DataMember(Name = "tc7ChangesByFinance")]
        public string TC7_CHANGES_BY_FINANCE { get; set; }
        [DataMember(Name = "tc8ChangesByRequestor")]
        public string TC8_CHANGES_BY_REQUESTOR { get; set; }
        [DataMember(Name = "tc8ChangesByFinance")]
        public string TC8_CHANGES_BY_FINANCE { get; set; }
        [DataMember(Name = "doneByCreative")]
        public bool? DONE_BY_CREATIVE { get; set; }
        [DataMember(Name = "doneByOutsideVendor")]
        public bool? DONE_BY_OUTSIDE_VENDOR { get; set; }
        [DataMember(Name = "isHotelVoucher")]
        public bool? IS_HOTEL_VOUCHER { get; set; }
        [DataMember(Name = "isGamingVoucher")]
        public bool? IS_GAMING_VOUCHER { get; set; }
    }
}
