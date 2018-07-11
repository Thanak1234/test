using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Entities.Forms
{
    [Table("ADDITIONAL_WORK", Schema = "HR")]
    public partial class AdditionalTimeWorked
    {
        [Key]
        public int Id { get; set; }
        [Column("RequestHeaderId")]
        public int? RequestHeaderId { get; set; }
        [Column("EmployeeId")]
        public int EmployeeId { get; set; }
        [Column("WorkingDate")]
        public DateTime? WorkingDate { get; set; }
        [Column("WorkOn")]
        public string WorkOn { get; set; }
        [Column("NumberOfHour")]
        public double? NumberOfHour { get; set; }
        [Column("Comment")]
        public string Comment { get; set; }
    }
}
