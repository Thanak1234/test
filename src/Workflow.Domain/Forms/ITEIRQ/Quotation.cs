using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Workflow.Domain.Entities.ITEIRQ
{
    [Table("ITEIRQ_QUOTATION", Schema = "IT")]
    public partial class Quotation
    {
        [Key]
        public int Id { get; set; }
        [Column("RequestHeaderId")]
        public int RequestHeaderId { get; set; }
        [Column("CompanyName")]
        public string CompanyName { get; set; }
        [Column("Validity")]
        public int? Validity { get; set; }
        [Column("DateIssued")]
        public DateTime? DateIssued { get; set; }
        [Column("Price")]
        public decimal? Price { get; set; }
    }
}