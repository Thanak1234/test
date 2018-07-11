using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Workflow.Domain.Entities.RMD
{
    [Table("Worksheet3")]
    public partial class Worksheet3
    {
        [Key]
        public int Id { get; set; }
        [Column("RequestHeaderId")]
        public int RequestHeaderId { get; set; }
        [Column("GrossRisk")]
        public string GrossRisk { get; set; }
        [Column("ControlReview")]
        public string ControlReview { get; set; }
        [Column("NetRisk")]
        public string NetRisk { get; set; }
        [Column("Gap")]
        public string Gap { get; set; }
        [Column("RiskTreatment")]
        public string RiskTreatment { get; set; }
        [Column("Tick")]
        public string Tick { get; set; }
        [Column("ActionPlan")]
        public string ActionPlan { get; set; }
        [Column("Completion")]
        public string Completion { get; set; }
        [Column("Percentage")]
        public double? Percentage { get; set; }
        [Column("Rational")]
        public string Rational { get; set; }
    }
}