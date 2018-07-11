using System.Collections.Generic;
using Workflow.Domain.Entities.VAF;
using Workflow.Domain.Entities.VoucherRequest;

namespace Workflow.Domain.Entities.BatchData
{
    public class VRWorkflowInstance : AbstractWorkflowInstance
    {
        public RequestData Information { get; set; }
    }
}