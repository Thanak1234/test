using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.INCIDENT
{

    [DataContract]
    public class IncidentEmployee
    {
        [DataMember(Name = "ID")]
        public int id { get; set; }

        [DataMember(Name = "EMPNO")]
        public string employeeno { get; set; }

        [DataMember(Name = "REQUEST_HEADER_ID")]
        public string requestheaderid { get; set; }
    }

    
}
