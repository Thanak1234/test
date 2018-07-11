using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Workflow.Domain.Entities.Finance
{
    [Table("ASSET_DISPOSAL_DETAIL", Schema = "FINANCE")]
    public partial class AssetDisposalDetail
    {
        [Key]
        public int Id { get; set; }
        [Column("RequestHeaderId")]
        public int? RequestHeaderId { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("SerialNo")]
        public string SerialNo { get; set; }
        [Column("Quantity")]
        public double? Quantity { get; set; }
        [Column("Location")]
        public string Location { get; set; }
        [Column("Reason")]
        public string Reason { get; set; }
        [Column("EstimatedRealisableValue")]
        public decimal? EstimatedRealisableValue { get; set; }
        
    }
}
