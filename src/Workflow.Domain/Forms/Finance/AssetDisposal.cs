using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Workflow.Domain.Entities.Finance
{
    [Table("ASSET_DISPOSAL", Schema = "FINANCE")]
    public partial class AssetDisposal
    {
        [Key]
        public int Id { get; set; }
        [Column("RequestHeaderId")]
        public int RequestHeaderId { get; set; }
        [Column("CoporationBranch")]
        public string CoporationBranch { get; set; }
        [Column("AssetGroupId")]
        public int? AssetGroupId { get; set; }
        [Column("TotalNetBookValue")]
        public decimal? TotalNetBookValue { get; set; }
    }
}
