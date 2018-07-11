using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Workflow.Domain.Entities.EGM
{
    [DataContract]
    public class MachineIssueType
    {
        
            [DataMember(Name = "id")]
            public int id { get; set; }

            [DataMember(Name = "type")]
            public string type { get; set; }

        
    }
}
