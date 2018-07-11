using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.HumanResource
{
    [DataContract]
    public class Requisition
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [IgnoreDataMember]
        public int RequestHeaderId { get; set; }
        
        [IgnoreDataMember]
        public virtual RequestHeader RequestHeader { get; set; }

        [DataMember(Name = "requestTypeId")]
        public int RequestTypeId { get; set; }

        [IgnoreDataMember]
        public virtual Lookup RequestType { get; set; }

        [DataMember(Name = "shiftTypeId")]
        public int ShiftTypeId { get; set; }

        [IgnoreDataMember]
        public virtual Lookup ShiftType { get; set; }

        [DataMember(Name = "locationTypeId")]
        public int LocationTypeId { get; set; }

        [IgnoreDataMember]
        public virtual Lookup LocationType { get; set; }

        [DataMember(Name = "referenceNo")]
        public string ReferenceNo { get; set; }

        [DataMember(Name = "position")]
        public string Position { get; set; }

        [DataMember(Name = "reportingTo")]
        public string ReportingTo { get; set; }

        [DataMember(Name = "salaryRange")]
        public string SalaryRange { get; set; }

        [DataMember(Name = "private")]
        public bool Private { get; set; }

        [DataMember(Name = "requisitionNumber")]
        public int RequisitionNumber { get; set; }

        [DataMember(Name = "justification")]
        public string Justification { get; set; }
        
    }
}
