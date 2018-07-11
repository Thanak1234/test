using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Workflow.Domain.Entities.RMD
{
    [Table("Worksheet2")]
    public partial class Worksheet2
    {
        [Key]
        public int Id { get; set; }
        [Column("RequestHeaderId")]
        public int RequestHeaderId { get; set; }

        // Sheet 2
        [Column("RiskActivity")]
        public string RiskActivity { get; set; }
        [Column("RiskCategory")]
        public string RiskCategory { get; set; }
        [Column("Descr")]
        public string Descr { get; set; }
        [Column("KeyRiskIndicator")]
        public string KeyRiskIndicator { get; set; }
        [Column("Consequence")]
        public string Consequence { get; set; }
        [Column("ControlActivity")]
        public string ControlActivity { get; set; }
        [Column("Responsibility")]
        public string Responsibility { get; set; }
        [Column("MonitoringProcess")]
        public string MonitoringProcess { get; set; }

        // Sheet 3
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
        [Column("Comment")]
        public string Comment { get; set; }
    }
}