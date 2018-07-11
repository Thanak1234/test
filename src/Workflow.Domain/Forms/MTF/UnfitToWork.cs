using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Workflow.Domain.Entities.MTF
{
    [DataContract]
    [Table("UNFIT_TO_WORK", Schema = "HR")]
    public partial class UnfitToWork
    {
        [Column("ID")]
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [Column("REQUEST_ID")]
        public int? RequestId { get; set; }
        [Column("UTW_DATE")]
        [DataMember]
        public DateTime? UtwDate { get; set; }
        [Column("STATUS")]
        [DataMember]
        public string Status { get; set; }
        [Column("NO_DAY")]
        [DataMember]
        public decimal? NoDay { get; set; }
    }
}
