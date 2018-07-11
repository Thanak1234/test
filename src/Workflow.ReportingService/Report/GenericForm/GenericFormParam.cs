using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    [DataContract]
    public class GenericFormParam : Parameter
    {
        [DataMember(Name = "RequestHeaderId")]
        public int RequestHeaderId {get; set;}

        [DataMember(Name = "Username")]
        public string Username { get; set; }

    }
}
