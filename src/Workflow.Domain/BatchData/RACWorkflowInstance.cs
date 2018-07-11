using Workflow.Domain.Entities.RAC;

namespace Workflow.Domain.Entities.BatchData {
    public class RACWorkflowInstance : AbstractWorkflowInstance
    {
        public AccessCard Information { get; set; }
    }
}