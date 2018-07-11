using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Entities.Forms
{
    [Table("EOM_BEST_PERFORMANCE_DETAIL", Schema = "HR")]
    public partial class BestPerformanceDetail
    {
        [Key]
        public int Id { get; set; }
        public int RequestHeaderId { get; set; }
        public int EmployeeId { get; set; }
        public string Gender { get; set; }
        public string Division { get; set; }
        public string ContactNo { get; set; }
        public string Type { get; set; }
    }
}
