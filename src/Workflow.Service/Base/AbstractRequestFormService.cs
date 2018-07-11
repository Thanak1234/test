/**
*@author : Phanny
*/

using Workflow.Business.Interfaces;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public abstract class AbstractRequestFormService<T,E> : IRequestFormService<E> where T : IRequestFormBC<E> where E: AbstractWorkflowInstance

    {

        protected T BC { get; set; }

        public E GetRequestInstanceData(E workflowInstance)
        {
            BC.WorkflowInstance = workflowInstance;
            BC.LoadData();
            return BC.WorkflowInstance;
            
        }

        public string TakeAction(E workflowInstance)
        {
            BC.WorkflowInstance = workflowInstance;
            return BC.TakeAction();           
        }

        public void SaveDraft(E workflowInstance)
        {

            BC.WorkflowInstance = workflowInstance;
            BC.SaveDraft();
        }

        public void ReleaseDraft(E workflowInstance)
        {
            BC.WorkflowInstance = workflowInstance;
            BC.ReleaseDraft();
        }
    }
}
