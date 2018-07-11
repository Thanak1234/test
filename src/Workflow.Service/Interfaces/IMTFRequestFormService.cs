using System;
using System.Collections.Generic;
using Workflow.Domain.Entities.MTF;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Service.Interfaces
{
    public interface IMTFRequestFormService : IRequestFormService<MTFRequestWorkflowInstance>
    {
        IEnumerable<Prescription> GetPrescriptionItems(int requestHeaderId);
    }
}
