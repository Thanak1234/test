using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Workflow.Domain.Entities.Finance
{
    [Table("ASSET_TRANSFER", Schema = "FINANCE")]
    public partial class AssetTransfer
    {
        [Key]
        public int Id { get; set; }
        [Column("RequestHeaderId")]
        public int? RequestHeaderId { get; set; }
        [Column("TransferToDeptId")]
        public int? TransferToDeptId { get; set; }
        [Column("CompanyBranch")]
        public string CompanyBranch { get; set; }
    }
}
