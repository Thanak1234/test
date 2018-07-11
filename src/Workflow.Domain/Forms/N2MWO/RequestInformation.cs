using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.N2MWO
{
    [DataContract]
    public class N2RequestInformation
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "requestHeaderId")]
        public int RequestHeaderId { get; set; }
        [DataMember(Name = "mode")]
        public string Mode { get; set; }
        [DataMember(Name = "requestType")]
        public string RequestType { get; set; }
        [DataMember(Name = "ccdId")]
        public int CcdId { get; set; }
        [DataMember(Name = "referenceNumber")]
        public string ReferenceNumber { get; set; }
        [DataMember(Name = "location")]
        public string Location { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "locationType")]
        public string LocationType { get; set; }
        [DataMember(Name = "subLocation")]
        public string SubLocation { get; set; }
    }

}
