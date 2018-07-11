using System.Runtime.Serialization;
using Workflow.Domain.Entities.Forms;

namespace Workflow.Web.Models.ATCF
{
    public class AdditionalTimeWorkedViewModel : AdditionalTimeWorked
    {
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
    }
}
