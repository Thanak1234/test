/**
*@author : Phanny
*/

using Workflow.Domain.Entities.HumanResource;

namespace Workflow.Domain.Entities.BatchData
{
    public class ERFRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public Requisition Requisition { get; set; }
    }
}
