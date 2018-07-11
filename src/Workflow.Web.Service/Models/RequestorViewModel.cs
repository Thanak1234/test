using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Web.Models
{
    public class RequestorViewModel
    {
        public int id { get; set; }
        public string employeeNo { get; set; }
        public string fullName { get; set; }
        public string position { get; set; }
        public string deptName { get; set; }
        public string subDept { get; set; }
        public string groupName { get; set; }
        public string devision { get; set; }
        public int priority { get; set; }
        public string phone { get; set; }
        public string ext { get; set; }
        public string email { get; set; }
        public string hod { get; set; }
        public string reportTo { get; set; }
        public DateTime? hiredDate { get; set; }

    }

}
