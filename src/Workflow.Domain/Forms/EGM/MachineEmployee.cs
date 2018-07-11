using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Workflow.Domain.Entities.EGM
{
    [DataContract]
    public class MachineEmployee
    {
        [DataMember(Name = "ID")]
        public int id { get; set; }
        
        [DataMember(Name = "EMPNO")]
        public string empno { get; set; }

    	[DataMember(Name = "REQUEST_HEADER_ID")]
        public int request_header_id { get; set; }        
    }
}
