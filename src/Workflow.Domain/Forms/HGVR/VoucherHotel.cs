using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Entities.Forms
{
    [Table("VoucherHotel")]
    public partial class VoucherHotel
    {
        [Key]
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public string PresentedTo { get; set; }
        public string InChargedDept { get; set; }
        public string Justification { get; set; }
    }
}
