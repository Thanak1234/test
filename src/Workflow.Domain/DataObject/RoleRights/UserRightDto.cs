using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Workflow.DataObject.RoleRights {

    [DataContract]
    public class UserRightDto {
        private string _fullName;

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "roleRightId")]
        public int RoleRightId { get; set; }

        [DataMember(Name = "loginName")]
        public string loginName { get; set; }

        [DataMember(Name = "employeeNo")]
        public string EmployeeNo { get; set; }

        [DataMember(Name = "fullName")]
        public string fullName {
            get {
                if (string.IsNullOrWhiteSpace(_fullName)) {
                    return this.EmployeeNo;
                }
                return _fullName;
            }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    _fullName = this.EmployeeNo;
                }
                _fullName = value;
            }
        }

        [DataMember(Name = "position")]
        public string Position { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "ext")]
        public string Ext { get; set; }

        [DataMember(Name = "phone")]
        public string phone { get; set; }

        [DataMember(Name = "reportTo")]
        public string ReportTo { get; set; }

        [DataMember(Name = "groupName")]
        public string GroupName { get; set; }

        [DataMember(Name = "subDeptId")]
        public int subDeptId { get; set; }

        [DataMember(Name = "subDept")]
        public string SubDept { get; set; }

        [DataMember(Name = "devision")]
        public string Devision { get; set; }

        [DataMember(Name = "hod")]
        public string HOD { get; set; }

        [DataMember(Name = "empType")]
        public string EmpType { get; set; }

        [DataMember(Name = "desc")]
        public string Desc { get; set; }

        [DataMember(Name = "darId")]
        public int DarId { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime? CreatedDate { get; set; }

        [DataMember(Name = "active")]
        public bool Active { get; set; }
    }

}
