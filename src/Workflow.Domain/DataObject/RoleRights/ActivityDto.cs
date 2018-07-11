using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Workflow.DataObject.RoleRights {

    [DataContract]
    public class ActivityDto {

        [DataMember(Name="id")]
        public int Id { get; set; }

        [DataMember(Name = "formId")]
        public int? FormId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
