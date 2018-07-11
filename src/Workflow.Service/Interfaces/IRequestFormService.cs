/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Service.Interfaces
{
    public interface IRequestFormService<T> where T : AbstractWorkflowInstance
    {
        string TakeAction(T workflowInstance);
        void SaveDraft(T workflowInstance);
        void ReleaseDraft(T workflowInstance);

        T GetRequestInstanceData(T workflowInstance);
    }
}
