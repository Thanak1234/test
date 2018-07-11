using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.N2MWO
{

    [DataContract]
    public class N2MaintenanceDepartment
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "requestHeaderId")]
        public int RequestHeaderId { get; set; }

        [DataMember(Name = "workTypeId")]
        public int WorkTypeId { get; set; }

        [DataMember(Name = "technicianId")]
        public int TechnicianId { get; set; }

        [DataMember(Name = "instruction")]
        public string Instruction { get; set; }

        [DataMember(Name = "assignDate")]
        public DateTime? AssignDate { get; set; }
    }
}
