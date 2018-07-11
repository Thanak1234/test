using System.Collections.Generic;
using Workflow.Domain.Entities.Admsr;
using Workflow.Domain.Entities.ITAD;

namespace Workflow.Domain.Entities.BatchData
{
    public class ITADRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public ITADEmployee Employee { get; set; }
    }
}
