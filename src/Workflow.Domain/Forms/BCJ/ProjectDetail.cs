using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.BCJ
{
    [DataContract]
    public class ProjectDetail
    {
        [IgnoreDataMember]
        private DateTime _modifiedDate;

        [IgnoreDataMember]
        private DateTime? _completionDate;

        [IgnoreDataMember]
        private DateTime? _commencementDate;

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [IgnoreDataMember]
        public int RequestHeaderId { get; set; }

        [DataMember(Name = "capexCategoryId")]
        public int CapexCategoryId { get; set; }

        [IgnoreDataMember]
        public virtual RequestHeader RequestHeader { get; set; }

        [IgnoreDataMember]
        public virtual CapexCategory CapexCategory { get; set; }

        [DataMember(Name = "projectName")]
        public string ProjectName { get; set; }

        [DataMember(Name = "reference")]
        public string Reference { get; set; }

        [DataMember(Name = "coporationBranch")]
        public string CoporationBranch { get; set; }

        [DataMember(Name = "whatToDo")]
        public string WhatToDo { get; set; }

        [DataMember(Name = "whyToDo")]
        public string WhyToDo { get; set; }

        [DataMember(Name = "arrangement")]
        public string Arrangement { get; set; }

        [DataMember(Name = "totalAmount")]
        public decimal TotalAmount { get; set; }

        [DataMember(Name = "estimateCapex")]
        public decimal EstimateCapex { get; set; }

        [DataMember(Name = "incrementalNetContribution")]
        public decimal? IncrementalNetContribution { get; set; }

        [DataMember(Name = "incrementalNetEbita")]
        public decimal? IncrementalNetEbita { get; set; }

        [DataMember(Name = "paybackYear")]
        public double? PaybackYear { get; set; }

        [DataMember(Name = "outlineBenefit")]
        public string OutlineBenefit { get; set; }

        [DataMember(Name = "commencement")]
        public DateTime? Commencement
        {
            get
            {
                return _commencementDate.Nullable();
            }
            set
            {
                _commencementDate = value.Nullable();
            }
        }

        [DataMember(Name = "completion")]
        public DateTime? Completion
        {
            get
            {
                return _completionDate.Nullable();
            }
            set
            {
                _completionDate = value.Nullable();
            }
        }

        [DataMember(Name = "alternative")]
        public string Alternative { get; set; }

        [DataMember(Name = "outlineRisk")]
        public string OutlineRisk { get; set; }

        [DataMember(Name = "capitalRequired")]
        public string CapitalRequired { get; set; }

        public DateTime ModifiedDate {
            get {
                return DateTime.Now;
            }
            set {
                _modifiedDate = value;
            }
        }
    }
}
