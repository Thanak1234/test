using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Entities.Finance
{
    [Table("ASSET_CONTROL_DETAIL", Schema = "FINANCE")]
    public partial class AssetControlDetail
    {
        [Key]
        public int Id { get; set; }
        [Column("RequestHeaderId")]
        public int? RequestHeaderId { get; set; }
        [Column("AssetDisposalDetailId")]
        public int? AssetDisposalDetailId { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("SerialNo")]
        public string SerialNo { get; set; }
        [Column("AssetNo")]
        public string AssetNo { get; set; }
        [Column("OriginalCost")]
        public decimal? OriginalCost { get; set; }
        [Column("DateOfPurchase")]
        public DateTime? DateOfPurchase { get; set; }
        [Column("NetBookValue")]
        public decimal? NetBookValue { get; set; }
        [Column("DateOfNBV")]
        public DateTime? DateOfNBV { get; set; }
        
    }
}
