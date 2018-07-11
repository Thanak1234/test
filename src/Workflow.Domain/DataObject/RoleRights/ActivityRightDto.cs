using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Workflow.DataObject.RoleRights {

    [DataContract]
    public class ActivityRightDto {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "employeeId")]
        public int EmployeeId { get; set; }

        [DataMember(Name = "form")]
        public string Form { get; set; }

        [DataMember(Name = "activity")]
        public string Activity { get; set; }

        [DataMember(Name = "role")]
        public string Role { get; set; }
    }

}
