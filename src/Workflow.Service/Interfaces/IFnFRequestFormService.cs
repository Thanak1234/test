using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Service.Interfaces
{
    public interface IFnFRequestFormService : IRequestFormService<FnFRequestWorkflowInstance>
    {
        int GetTotalRoomNightTaken(DateTime checkinDate, DateTime checkoutDate, int numberOfRoom, int requestorId);
        int GetTotalRoomNightTaken(int requestHeaderId);
    }
}
