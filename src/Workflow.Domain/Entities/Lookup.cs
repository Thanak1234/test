using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities
{
    [DataContract]
    public class Lookup
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "parentId")]
        public int ParentId { get; set; }

        [IgnoreDataMember]
        public virtual Lookup Parent { get; set; }

        [IgnoreDataMember]
        public int FormId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "hasChild")]
        public bool HasChild { get; set; }

        [DataMember(Name = "sequence")]
        public int Sequence { get; set; }

        [DataMember(Name = "active")]
        public bool Active { get; set; }

    }

    [DataContract]
    public class BasicLookup
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }
    }
}
