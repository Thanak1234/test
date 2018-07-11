using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.HR
{
    [DataContract]
    public class TravelDetail
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "classTravelEntitlement")]
        public byte ClassTravelEntitlement { get; set; }

        [DataMember(Name = "classTravelRequest")]
        public byte ClassTravelRequest { get; set; }

        [DataMember(Name = "reasonException")]
        public string ReasonException { get; set; }

        [DataMember(Name = "purposeTravel")]
        public byte PurposeTravel { get; set; }

        [DataMember(Name = "estimatedCostTicket")]
        public decimal EstimatedCostTicket { get; set; }

        [DataMember(Name = "requestHeaderId")]
        public int RequestHeaderId { get; set; }

        [DataMember(Name = "costTicket")]
        public decimal costTicket { get; set; }

        [DataMember(Name = "reasonTravel")]
        public string ReasonTravel { get; set; }

        [DataMember(Name = "noRequestTaken")]
        public int NoRequestTaken { get; set; }

        [DataMember(Name = "extraCharge")]
        public decimal ExtraCharge { get; set; }

        [DataMember(Name = "remark")]
        public string Remark { get; set; }
    }
}
