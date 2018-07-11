using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Workflow.Domain.Entities.MTF
{
    [Table("TREATMENT", Schema = "HR")]
    public partial class Treatment
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("REQUEST_HEADER_ID")]
        public int? RequestHeaderId { get; set; }
        public RequestHeader RequestHeader { get; set; }
        [Column("FIT_TO_WORK")]
        public int? FitToWork { get; set; }
        [Column("TIME_ARRIVED")]
        public DateTime? TimeArrived { get; set; }
        [Column("TIME_DEPARTED")]
        public DateTime? TimeDeparted { get; set; }
        [Column("DAYS")]
        public decimal? Days { get; set; }
        [Column("REMARK")]
        public string Remark { get; set; }
        [Column("SYMPTOM")]
        public string Symptom { get; set; }
        [Column("DIAGNOSIS")]
        public string Diagnosis { get; set; }
        [Column("HOURS")]
        public DateTime? Hours { get; set; }
        [Column("COMMENT")]
        public string Comment { get; set; }
        [Column("WORK_SHIFT")]
        public string WorkShift { get; set; }
        [Column("CHECK_IN_DATE")]
        public DateTime? CheckInDateTime { get; set; }
        [Column("CHECK_OUT_DATE")]
        public DateTime? CheckOutDateTime { get; set; }
    }
}
