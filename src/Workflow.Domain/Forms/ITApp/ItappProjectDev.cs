using System;

namespace Workflow.Domain.Entities.Core.ITApp
{
    public class ItappProjectDev
    {
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsQA { get; set; }
        public string Remark { get; set; }
    }
}
