using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Workflow.DataObject.Roles {

    [DataContract]
    public class UserRoleDto {

        [DataMember(Name = "fullName")]
        public string fullName { get; set; }

        [DataMember(Name = "empId")]
        public int? EmpId { get; set; }

        [DataMember(Name = "employeeNo")]
        public string EmployeeNo { get; set; }

        [DataMember(Name = "position")]
        public string Position { get; set; }

        [DataMember(Name = "subDept")]
        public string SubDept { get; set; }
        
        [DataMember(Name = "groupName")]
        public string GroupName { get; set; }

        [DataMember(Name = "devision")]
        public string Devision { get; set; }

        [DataMember(Name = "loginName")]
        public string LoginName { get; set; }

        [DataMember(Name = "include")]
        public bool Include { get; set; }

    }

}