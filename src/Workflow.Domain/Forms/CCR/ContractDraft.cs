using System;

namespace Workflow.Domain.Entities.Core.CCR
{
    public class ContractDraft
    {
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public string Name { get; set; }
        public string Vat { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string RegistrationNo { get; set; }
        public string Phone { get; set; }
        public string ContactName { get; set; }
        public string Position { get; set; }
        public string IssueedBy { get; set; }
        public string Term { get; set; }
        public DateTime? StartDate { get; set; }
        public string InclusiveTax { get; set; }
        public DateTime? EndingDate { get; set; }
        public string PaymentTerm { get; set; }
        public bool? AtSa { get; set; }
        public bool? AtSpa { get; set; }
        public bool? AtLa { get; set; }
        public bool? AtCa { get; set; }
        public bool? AtLea { get; set; }
        public bool? AtEa { get; set; }
        public string AtOther { get; set; }
        public bool? Vis { get; set; }
        public bool? StatusNew { get; set; }
        public bool? StatusRenewal { get; set; }
        public bool? StatusReplacement { get; set; }
        public bool? StatusAddendum { get; set; }
        public bool? IsCapex { get; set; }
        public string BcjNumber { get; set; }
        public string ActA { get; set; }
        public string ActB { get; set; }
        public string ActC { get; set; }
        public string ActD { get; set; }
    }
}
