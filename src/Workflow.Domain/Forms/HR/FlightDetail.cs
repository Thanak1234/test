using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.HR
{
    [DataContract]
    public class FlightDetail
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "fromDestination")]
        public string FromDestination { get; set; }

        [DataMember(Name ="toDestination")]
        public string ToDestination { get; set; }

        [DataMember(Name ="date")]
        public DateTime Date { get; set;}

        [DataMember(Name ="time")]
        public DateTime Time { get; set; }
        
        [DataMember(Name ="requestHeaderId")]
        public int RequestHeaderId { get; set; }
    }
}
