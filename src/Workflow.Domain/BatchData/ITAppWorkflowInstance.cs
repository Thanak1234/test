
using System.Collections.Generic;
using Workflow.Domain.Entities.ADMCPPForm;
using Workflow.Domain.Entities.Core.ITApp;

namespace Workflow.Domain.Entities.BatchData
{
    public class ITAppWorkflowInstance : AbstractWorkflowInstance
    {
        public ItappProjectApproval ProjectApproval { get; set; }
        public ItappProjectDev ProjectDev { get; set; }
        public ItappProjectDev ProjectQA { get; set; }
        public ItappProjectInit ProjectInit { get; set; }
    }
}