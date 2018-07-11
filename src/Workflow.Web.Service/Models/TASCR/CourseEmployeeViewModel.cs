using System.Runtime.Serialization;
using Workflow.Domain.Entities.Forms;

namespace Workflow.Web.Models.TAS
{
    public class CourseEmployeeViewModel : CourseEmployee
    {
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
    }
}
