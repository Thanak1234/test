using System.Collections.Generic;
using Workflow.Domain.Entities.Admsr;
using Workflow.Domain.Entities.ITAD;
using Workflow.Domain.Entities.ITCR;

namespace Workflow.Domain.Entities.BatchData
{
    public class ITCRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public RequestFormData FormData { get; set; }
    }
}
