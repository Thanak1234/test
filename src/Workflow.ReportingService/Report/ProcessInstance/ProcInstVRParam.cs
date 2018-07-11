using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class ProcInstVRParam : ProcInstParam
    {
        [DataMember(Name = "ExportType")]
        public string ExportType { get; set; }
        [DataMember(Name = "VoucherType")]
        public string VoucherType { get; set; }
        [DataMember(Name = "QtyRequest")]
        public int? QtyRequest { get; set; }
        [DataMember(Name = "VoucherNo")]
        public string VoucherNo { get; set; }
        [DataMember(Name = "VoucherHeader")]
        public string VoucherHeader { get; set; }
        [DataMember(Name = "IsHotelVoucher")]
        public bool? IsHotelVoucher { get; set; }
        [DataMember(Name = "IsGamingVoucher")]
        public bool? IsGamingVoucher { get; set; }
        [DataMember(Name = "IsReprint")]
        public bool? IsReprint { get; set; }
    }
}
