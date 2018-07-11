using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    public class CCRProcInst : ProcInst
    {
        [DataMember(Name = "name")]
        public string NAME { get; set; }
        [DataMember(Name = "vat")]
        public string VAT { get; set; }
        [DataMember(Name = "address")]
        public string ADDRESS { get; set; }
        [DataMember(Name = "email")]
        public string EMAIL { get; set; }
        [DataMember(Name = "registrationNo")]
        public string REGISTRATION_NO { get; set; }
        [DataMember(Name = "phone")]
        public string PHONE { get; set; }
        [DataMember(Name = "contactName")]
        public string CONTACT_NAME { get; set; }
        [DataMember(Name = "position")]
        public string POSITION { get; set; }
        [DataMember(Name = "issueedBy")]
        public string ISSUEED_BY { get; set; }
        [DataMember(Name = "term")]
        public string TERM { get; set; }
        [DataMember(Name = "startDate")]
        public DateTime? START_DATE { get; set; }
        [DataMember(Name = "inclusiveTax")]
        public string INCLUSIVE_TAX { get; set; }
        [DataMember(Name = "endingDate")]
        public DateTime ENDING_DATE { get; set; }
        [DataMember(Name = "paymentTerm")]
        public string PAYMENT_TERM { get; set; }
        [DataMember(Name = "atSa")]
        public bool? AT_SA { get; set; }
        [DataMember(Name = "atSpa")]
        public bool? AT_SPA { get; set; }
        [DataMember(Name = "atLa")]
        public bool? AT_LA { get; set; }
        [DataMember(Name = "atCa")]
        public bool? AT_CA { get; set; }
        [DataMember(Name = "atLea")]
        public bool? AT_LEA { get; set; }
        [DataMember(Name = "atEa")]
        public bool? AT_EA { get; set; }
        [DataMember(Name = "statusNew")]
        public bool? STATUS_NEW { get; set; }
        [DataMember(Name = "statusRenewal")]
        public bool? STATUS_RENEWAL { get; set; }
        [DataMember(Name = "statusReplacement")]
        public bool? STATUS_REPLACEMENT { get; set; }
        [DataMember(Name = "statusAddendum")]
        public bool? STATUS_ADDENDUM { get; set; }
        [DataMember(Name = "atOther")]
        public string AT_OTHER { get; set; }
        [DataMember(Name = "visStatus")]
        public string VIS_STATUS { get; set; }
        [DataMember(Name = "vis")]
        public bool? VIS { get; set; }
        [DataMember(Name = "isCapex")]
        public bool? IS_CAPEX { get; set; }
        [DataMember(Name = "bcjNumber")]
        public string BCJ_NUMBER { get; set; }
        [DataMember(Name = "actA")]
        public string ACT_A { get; set; }
        [DataMember(Name = "actB")]
        public string ACT_B { get; set; }
        [DataMember(Name = "actC")]
        public string ACT_C { get; set; }
        [DataMember(Name = "actD")]
        public string ACT_D { get; set; }
    }
}
