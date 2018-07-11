using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Entities.Forms
{
    [Table("EOM_BEST_PERFORMANCE", Schema = "HR")]
    public partial class BestPerformance
    {
        [Key]
        public int Id { get; set; }
        public int RequestHeaderId { get; set; }
        public DateTime? EmployeeOfMonth { get; set; }
        public decimal? EOMAward { get; set; }
        public decimal? BestPerformanceAward { get; set; }
    }
}
