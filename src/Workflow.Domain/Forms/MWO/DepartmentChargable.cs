using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.MWO {

    [DataContract]
    public class DepartmentChargable {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "ccd")]
        public string CCD { get; set; }

        [DataMember(Name = "loc")]
        public string Location { get; set; }

        [DataMember(Name = "department")]
        public string Department { get; set; }

        [DataMember(Name = "sequence")]
        public int Sequence { get; set; }

        [DataMember(Name = "currentNumber")]
        public int CurrentNumber { get; set; }
    }

}
