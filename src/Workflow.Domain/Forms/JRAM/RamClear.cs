using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Entities.Forms
{
    [Table("RamClear")]
    public partial class RamClear
    {
        [Key]
        public int Id { get; set; }
        [Column("RequestHeaderId")]
        public int? RequestHeaderId { get; set; }
        [Column("Props")]
        public string Props { get; set; }
        [Column("Gmid")]
        public string Gmid { get; set; }
        [Column("Game")]
        public string Game { get; set; }
        [Column("Rtp")]
        public double? Rtp { get; set; }
        [Column("ClearDate")]
        public DateTime? ClearDate { get; set; }
        [Column("Instances")]
        public string Instances { get; set; }
        [Column("CheckList")]
        public string CheckList { get; set; }
        [Column("Descr")]
        public string Descr { get; set; }
    }
}
