using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.ITAD
{
    public class ITADEmployee
    {
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Location { get; set; }
        public bool? NoEmail { get; set; }
        public string Remark { get; set; }
        public string Email { get; set; }
    }
}
