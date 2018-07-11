using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities
{
    [DataContract]
    public class ActivityConfig
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "requestCode")]
        public string RequestCode { get; set; }

        [DataMember(Name = "activity")]
        public string Activity { get; set; }

        [DataMember(Name = "actions")]
        public string[] Actions { get; set; }
    }
}
