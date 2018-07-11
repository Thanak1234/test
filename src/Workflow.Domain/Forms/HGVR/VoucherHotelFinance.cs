using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Entities.Forms
{
    [Table("VoucherHotelFinance")]
    public partial class VoucherHotelFinance
    {
        [Key]
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public string IssuedNo { get; set; }
        public string EntitiesBearerTo { get; set; }
        public string InChargedDept { get; set; }
    }
}
