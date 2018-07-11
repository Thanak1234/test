using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Workflow.Domain.Entities.MTF
{
    [DataContract]
    [Table("PRESCRIPTION", Schema = "HR")]
    public partial class Prescription
    {
        [Column("ID")]
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [Column("QTY")]
        [DataMember(Name = "quantity")]
        public int? Qty { get; set; }
        [Column("USAGE")]
        [DataMember(Name = "usage")]
        public string Usage { get; set; }
        [Column("TREATMENT_ID")]
        [DataMember(Name = "treatmentId")]
        public int? TreatmentId { get; set; }
        [Column("MEDICINE_ID")]
        [DataMember(Name = "medicineId")]
        public int MedicineId { get; set; }
    }
}
