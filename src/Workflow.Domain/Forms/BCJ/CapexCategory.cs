using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.BCJ
{
    [DataContract]
    public class CapexCategory
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "vouching")]
        public string Vouching { get; set; }

        [DataMember(Name = "departmentId")]
        public int DepartmentId { get; set; }

    }
}
