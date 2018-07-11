using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.VAF {
    public class Outline {
        public int Id { get; set; }
        public int RequestHeaderId { get; set; }
        public DateTime? GamingDate { get; set; }
        public string McidLocn { get; set; }
        public string VarianceType { get; set; }
        public string Subject { get; set; }
        public string IncidentRptRef { get; set; }
        public Int64? ProcessInstanceId { get; set; }
        public string Area { get; set; }
        public string RptComparison { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
    }
}
