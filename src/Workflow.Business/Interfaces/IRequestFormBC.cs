/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Business.Interfaces
{

    public interface IRequestFormBC<T> where T : AbstractWorkflowInstance
    {

        T WorkflowInstance { get; set; }

        void LoadData();

        string TakeAction();
        
        void SaveDraft();

        void ReleaseDraft();


    }

    
}
