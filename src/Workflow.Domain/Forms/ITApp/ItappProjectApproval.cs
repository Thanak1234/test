using System;

namespace Workflow.Domain.Entities.Core.ITApp
{
    public class ItappProjectApproval
    {
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public decimal? Hc { get; set; }
        public decimal? Slc { get; set; }
        public int? Scmd { get; set; }
        public string Rsim { get; set; }
        public string Rawm { get; set; }
        public string DeliveryDate { get; set; }
        public DateTime? GoLiveDate { get; set; }
    }
}
