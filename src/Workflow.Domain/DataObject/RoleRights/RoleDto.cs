using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Workflow.DataObject.RoleRights {
    [DataContract]
    public class RoleDto {

        [DataMember(Name="id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name="displayName")]
        public string DisplayName { get; set; }

        [DataMember(Name="description")]
        public string Description { get; set; }
    }

}
