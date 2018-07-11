using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Workflow.Domain.Entities.MTF
{
    [Table("MEDICINE", Schema = "HR")]
    public partial class Medicine
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("ITEM_CODE")]
        public string ItemCode { get; set; }
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        [Column("ONHAND")]
        public double? Onhand { get; set; }
        [Column("COST")]
        public double? Cost { get; set; }
        [Column("AMOUNT")]
        public double? Amount { get; set; }
        [Column("ACTIVE")]
        public bool? Active { get; set; }
    }
}
