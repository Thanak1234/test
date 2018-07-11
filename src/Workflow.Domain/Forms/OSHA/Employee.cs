using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.OSHA
{
    public class OSHAEmployee
    {
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string DeptName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string EmpType { get; set; }
    }
}
