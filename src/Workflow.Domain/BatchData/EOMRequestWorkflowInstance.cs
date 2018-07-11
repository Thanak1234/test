namespace Workflow.Domain.Entities.BatchData
{
    public class EOMRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public EOMRequestForm.EOMDetail EOMInfo { get; set; }
    }
}
