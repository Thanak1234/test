using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject
{
    public class EmployeeDto
    {

        private DepartmentDto _department =null;
        private string _fullName;

        public int id { get; set; }
        public string loginName { get; set; }
        public string employeeNo { get; set; }
        public string fullName {
            get
            {
                if (string.IsNullOrWhiteSpace(_fullName))
                {
                    return this.employeeNo;
                }
                return _fullName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) {
                    _fullName = this.employeeNo;
                }
                _fullName = value;
            }
        }
        public string position { get; set; }
        public string email { get; set; }
        public string ext { get; set; }
        public string phone { get; set; } 
        public string reportTo { get; set; }
        public string groupName { get; set; }
        public int subDeptId { get; set; }
        public DepartmentDto department
        {
            get
            {

                return _department ?? (_department = new DepartmentDto() {
                    id = subDeptId,
                    fullName = subDept
                });
            }
        }
        public string subDept { get; set; }
        public string deptName { get; set; }
        public string devision { get; set; }
        public string hod { get; set; }
        public string empType { get; set; }
        public bool isAdmin { get; set; }
        public string roles { get; set; }
    }
}
