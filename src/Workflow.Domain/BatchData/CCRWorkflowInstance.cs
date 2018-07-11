using System.Collections.Generic;
using Workflow.Domain.Entities.Core.CCR;
using Workflow.Domain.Entities.VAF;
using Workflow.Domain.Entities.VoucherRequest;

namespace Workflow.Domain.Entities.BatchData
{
    public class CCRWorkflowInstance : AbstractWorkflowInstance
    {
        public ContractDraft ContractDraft { get; set; }
    }
}