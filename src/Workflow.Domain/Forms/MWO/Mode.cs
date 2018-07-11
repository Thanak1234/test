using System;
using System.Runtime.Serialization;

namespace Workflow.Domain.Entities.MWO {

    [DataContract]
    public class Mode {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "sequence")]
        public int Sequence { get; set; }
    }

}
