using System;
using System.Runtime.Serialization;

namespace Workflow.Domain.Entities.N2MWO
{

    [DataContract]
    public class N2Mode
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "sequence")]
        public int Sequence { get; set; }
    }

}
