using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject
{
    public class DepartmentDto
    {
        public int id { get; set; }
        public int deptId { get; set; }
        public int groupId { get; set; }
        public string teamCode { get; set; }
        public string teamName { get; set; }
        public string deptCode { get; set; }
        public string deptName { get; set; }
        public string groupCode { get; set; }
        public string groupName { get; set; }
        public string deptType { get; set; }
        public string fullName { get; set; }
             
    }
}
