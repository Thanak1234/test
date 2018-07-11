using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Entities.RMD
{
    [Table("RiskAssessment")]
    public partial class RiskAssessment
    {
        [Key]
        public int Id { get; set; }
        [Column("RequestHeaderId")]
        public int? RequestHeaderId { get; set; }
        [Column("BusinessUnit")]
        public string BusinessUnit { get; set; }
        [Column("Objective")]
        public string Objective { get; set; }
    }
}