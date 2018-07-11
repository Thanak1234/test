using System;
using System.Collections.Generic;
using Workflow.Domain.Entities.Forms;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Service.Interfaces
{
    public interface IATCFRequestFormService : IRequestFormService<ATCFRequestWorkflowInstance>
    {
        IEnumerable<AdditionalTimeWorked> GetAdditionalTimeWorkeds(int requestHeaderId);
    }
}
