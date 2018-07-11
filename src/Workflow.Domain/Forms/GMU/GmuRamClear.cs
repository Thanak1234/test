using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Entities.Forms
{
    [Table("GmuRamClear")]
    public partial class GmuRamClear
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
        [Column("Ip")]
        public string Ip { get; set; }
        [Column("ClearDate")]
        public DateTime? ClearDate { get; set; }
        [Column("Gmus")]
        public string Gmus { get; set; }
        [Column("CheckList")]
        public string CheckList { get; set; }
        [Column("Descr")]
        public string Descr { get; set; }
        [Column("MacAddress")]
        public string MacAddress { get; set; }
        [Column("Remark")]
        public string Remark { get; set; }
    }
}
