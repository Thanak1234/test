using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Workflow.Domain.Entities.RMD
{
    [Table("Worksheet1")]
    public partial class Worksheet1
    {
        [Key]
        public int Id { get; set; }
        [Column("RequestHeaderId")]
        public int RequestHeaderId { get; set; }
        [Column("BusinessProcess")]
        public string BusinessProcess { get; set; }
        [Column("Activities")]
        public string Activities { get; set; }
    }
}