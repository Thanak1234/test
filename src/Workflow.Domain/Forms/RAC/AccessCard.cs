using System;

namespace Workflow.Domain.Entities.RAC
{
    public class AccessCard
    {
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public string Item { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        public DateTime? IssueDate { get; set; }
        public string SerialNo { get; set; }
    }
}
