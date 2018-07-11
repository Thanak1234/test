using System;

namespace Workflow.Domain.Entities.Core.ITApp
{
    public class ItappProjectInit
    {
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public string Application { get; set; }
        public string ProposedChange { get; set; }
        public bool BenefitCS { get; set; }
        public bool BenefitIIS { get; set; }
        public bool BenefitRM { get; set; }
        public string BenefitOther { get; set; }
        public decimal? PriorityConsideration { get; set; }
        public string Description { get; set; }
    }
}
