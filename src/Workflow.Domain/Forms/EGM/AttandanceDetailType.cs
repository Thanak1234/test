using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.EGM
{
    [DataContract]
    public class AttandanceDetailType
    {

        
        
            [DataMember(Name = "id")]
            public int id { get; set; }

            [DataMember(Name = "type")]
            public string type { get; set; }            
      

    }
}
