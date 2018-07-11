using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Workflow.DataObject.Roles {
    [DataContract]
    public class WorkflowRoleDto {

        [DataMember(Name = "workflow")] 
        public string Workflow { get; set; }

        [DataMember(Name = "activity")]
        public string Activity { get; set; }

        [DataMember(Name = "fullName")]
        public string FullName { get; set; }

        [DataMember(Name = "role")]
        public string Role { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
