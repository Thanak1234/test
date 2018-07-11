using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Web.Models.DepartmentRight
{

    public class DeptRightViewModel
    {
        public int id { get; set; }
        public int formid { get; set; }
        public int deptid { get; set; }
        public bool active { get; set; }
        public string empno { get; set; }
    }

}
