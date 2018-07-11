using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Entities.ITEIRQ
{
    [Table("ITEIRQ", Schema = "IT")]
    public partial class EventInternet
    {
        [Key]
        public int Id { get; set; }
        [Column("RequestHeaderId")]
        public int? RequestHeaderId { get; set; }
        [Column("Subject")]
        public string Subject { get; set; }
        [Column("StartDate")]
        public DateTime? StartDate { get; set; }
        [Column("EndDate")]
        public DateTime? EndDate { get; set; }
        [Column("Bandwidth")]
        public int? Bandwidth { get; set; }
        [Column("Cost")]
        public decimal? Cost { get; set; }
        [Column("RequestDescr")]
        public string requestDescr { get; set; }
        [Column("ITComment")]
        public string Comment { get; set; }
    }
}