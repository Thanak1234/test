using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketAgentDto
    {
        private DepartmentDto _department = null;
        private string _fullName;


        [DataMember(Name = "id")]
        public int id { get; set; }
        
        

        [DataMember(Name = "accountType")]
        public string accountType { get; set; }

        [DataMember(Name = "accountTypeId")]
        public int accountTypeId { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "statusId")]
        public int statusId { get; set; }

        [DataMember(Name = "groupPolicyGroupName")]
        public string groupPolicyGroupName { get; set; }
        
        [DataMember(Name = "groupPolicyId")]
        public int groupPolicyId { get; set; }

        [DataMember(Name = "deptName")]
        public string deptName { get; set; }

        [DataMember(Name = "deptId")]
        public int deptId { get; set; }

        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime createdDate { get; set; }

        [DataMember(Name = "modifiedDate")]
        public DateTime modifiedDate { get; set; }


        public string loginName { get; set; }
        public string employeeNo { get; set; }
        public string fullName
        {
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
                if (string.IsNullOrWhiteSpace(value))
                {
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

                return _department ?? (_department = new DepartmentDto()
                {
                    id = subDeptId,
                    fullName = subDept
                });
            }
        }
        public string subDept { get; set; }
        public string devision { get; set; }
        public string hod { get; set; }
        public string empType { get; set; }
        public int empId { get; set; }

    }
}
