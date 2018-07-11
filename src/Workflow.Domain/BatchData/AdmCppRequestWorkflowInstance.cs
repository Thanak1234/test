
using System.Collections.Generic;
using Workflow.Domain.Entities.ADMCPPForm;

namespace Workflow.Domain.Entities.BatchData
{
    public class AdmCppRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public ADMCPP AdmCpp { get; set; }
    }
}