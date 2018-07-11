using System.ComponentModel.DataAnnotations.Schema;
namespace Workflow.Web.Service.Models
{
    [Table("PATIENT", Schema = "QUEUE")]
    public partial class PatientQueue
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("REQUEST_HEADER_ID")]
        public int RequestHeaderId { get; set; }
        [Column("FINGER_PRINT_ID")]
        public int FingerPrintId { get; set; }
        [Column("STATUS")]
        public string Status { get; set; }
        [Column("PRIORITY")]
        public int Priority { get; set; }
    }
}