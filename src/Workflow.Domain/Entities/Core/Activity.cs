namespace Workflow.Domain.Entities.Core
{
    public partial class Activity
    {
        public int Id { get; set; }

        public int? WorkflowId { get; set; }

        public string Type { get; set; }
        
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string ActCode { get; set; }
        
        public string Property { get; set; }
        
        public int? Sequence { get; set; }

        public bool? Active { get; set; }

    }
}
