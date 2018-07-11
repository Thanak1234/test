using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Entities.Forms
{
    [Table("VoucherHotelDetail")]
    public partial class VoucherHotelDetail
    {
        [Key]
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public int? QuantityRequest { get; set; }
        public string EntitiesBearerTo { get; set; }
        public DateTime? ValidDateFrom { get; set; }
        public DateTime? ValidDateTo { get; set; }
        public double? TotalCashCollected { get; set; }
        public DateTime? DateRequired { get; set; }
    }
}
