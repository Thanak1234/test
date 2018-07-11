/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.PBF
{
    [DataContract]
    public class ProjectBrief
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [IgnoreDataMember]
        public int RequestHeaderId { get; set; }
        [IgnoreDataMember]
        public RequestHeader RequestHeader { get; set; }
        [DataMember(Name = "projectName")]
        public string ProjectName { get; set; }
        [DataMember(Name = "businessUnit")]
        public string BusinessUnit { get; set; }
        [DataMember(Name = "projectLead")]
        public string ProjectLead { get; set; }
        [DataMember(Name = "accountManager")]
        public string AccountManager { get; set; }
        [DataMember(Name = "budget")]
        public decimal Budget { get; set; }
        [DataMember(Name = "billingInfo")]
        public string BillingInfo { get; set; }
        [DataMember(Name = "submissionDate")]
        public DateTime? SubmissionDate
        {
            get
            {
                return _submissionDate.Nullable();
            }
            set
            {
                _submissionDate = value.Nullable();
            }
        }
        [DataMember(Name = "requiredDate")]
        public DateTime? RequiredDate
        {
            get
            {
                return _requiredDate.Nullable();
            }
            set
            {
                _requiredDate = value.Nullable();
            }
        }
        [DataMember(Name = "actualEventDate")]
        public DateTime? ActualEventDate
        {
            get
            {
                return _actualEventDate.Nullable();
            }
            set
            {
                _actualEventDate = value.Nullable();
            }
        }
        [DataMember(Name = "introduction")]
        public string Introduction { get; set; }
        [DataMember(Name = "targetMarket")]
        public string TargetMarket { get; set; }
        [DataMember(Name = "usage")]
        public string Usage { get; set; }
        [DataMember(Name = "briefing")]
        public string Briefing { get; set; }
        [DataMember(Name = "designDuration")]
        public string DesignDuration { get; set; }
        [DataMember(Name = "productDuration")]
        public string ProductDuration { get; set; }
        [DataMember(Name = "dateline")]
        public DateTime? Dateline
        {
            get
            {
                return _dateline.Nullable();
            }
            set
            {
                _dateline = value.Nullable();
            }
        }
        [DataMember(Name = "brainStorm")]
        public DateTime? BrainStorm
        {
            get
            {
                return _brainStorm.Nullable();
            }
            set
            {
                _brainStorm = value.Nullable();
            }
        }
        [DataMember(Name = "projectStart")]
        public DateTime? ProjectStart
        {
            get
            {
                return _projectStart.Nullable();
            }
            set
            {
                _projectStart = value.Nullable();
            }
        }
        [DataMember(Name = "firstRevision")]
        public DateTime? FirstRevision
        {
            get
            {
                return _firstRevision.Nullable();
            }
            set
            {
                _firstRevision = value.Nullable();
            }
        }
        [DataMember(Name = "secondRevision")]
        public DateTime? SecondRevision
        {
            get
            {
                return _secondRevision.Nullable();
            }
            set
            {
                _secondRevision = value.Nullable();
            }
        }
        [DataMember(Name = "finalApproval")]
        public DateTime? FinalApproval
        {
            get
            {
                return _finalApproval.Nullable();
            }
            set
            {
                _finalApproval = value.Nullable();
            }
        }
        [DataMember(Name = "completion")]
        public DateTime? Completion
        {
            get
            {
                return _completion.Nullable();
            }
            set
            {
                _completion = value.Nullable();
            }
        }
        [DataMember(Name = "projectReference")]
        public string ProjectReference { get; set; }
        [DataMember(Name = "draftComment")]
        public string DraftComment { get; set; }
        
        private DateTime? _submissionDate;
        private DateTime? _requiredDate;
        private DateTime? _actualEventDate;
        private DateTime? _dateline;
        private DateTime? _brainStorm;
        private DateTime? _projectStart;
        private DateTime? _firstRevision;
        private DateTime? _secondRevision;
        private DateTime? _finalApproval;
        private DateTime? _completion;
    }
}
