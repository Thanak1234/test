using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Workflow.Domain.Entities.Finance
{
    [Table("ASSET_TRANSFER_DETAIL", Schema = "FINANCE")]
    public partial class AssetTransferDetail
    {
        [Key]
        public int Id { get; set; }
        [Column("RequestHeaderId")]
        public int? RequestHeaderId { get; set; }
        [Column("TransferCode")]
        public string TransferCode { get; set; }
        [Column("NetBookValue")]
        public decimal? NetBookValue { get; set; }
        [Column("DateOfPurchase")]
        public DateTime? DateOfPurchase { get; set; }
        [Column("Description")]
        public string Description { get; set; }
    }
}
