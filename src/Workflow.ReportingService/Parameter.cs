using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService
{
    [DataContract]
    public class Parameter
    {

        [DataMember(Name = "Start")]
        public int Skip { get; set; }

        [DataMember(Name = "Page")]
        public int Page { get; set; }

        [DataMember(Name = "Limit")]
        public int Take { get; set; }
    }
}
