using System.Runtime.Serialization;
using Workflow.Domain.Entities.Forms;

namespace Workflow.Web.Models.EOMBP
{
    public class EmployeeOfMonthDetailViewModel : BestPerformanceDetail
    {
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
    }
}
