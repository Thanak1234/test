using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.DepartmentRight
{
    public class DeptAccessRightParam : QueryParameter
    {
        public int formid { get; set; }
        public int deptid { get; set; }
    }
}
