using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject
{
    [DataContract]
    public class ResponseText
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "show")]
        public bool Show { get; set; }
    }
}
