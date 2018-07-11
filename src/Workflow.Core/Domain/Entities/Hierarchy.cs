using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Workflow.Core.Entities
{
    [DataContract]
    public class Hierarchy
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "parentId")]
        public int ParentId { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "leaf")]
        public bool Leaf { get; set; }

        [DataMember(Name = "children")]
        public IEnumerable<object> Children { get; set; }

        [DataMember(Name = "level")]
        public int Level { get; set; }
    }
}