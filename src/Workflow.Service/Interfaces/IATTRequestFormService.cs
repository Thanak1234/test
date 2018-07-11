using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Service.Interfaces
{
    public interface IATTRequestFormService:IRequestFormService<ATTRequestWorkflowInstance>
    {
        int GetTakenYears(int requestorId, int purposeId);
    }
}
