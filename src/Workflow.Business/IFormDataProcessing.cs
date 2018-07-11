/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business
{
    public interface IFormDataProcessing
    {
        bool IsAddNewRequestHeader{ get; set; }
        bool IsUpdateLastActivity { get; set; }
        bool IsEditRequestor { get; set; }
        bool IsEditPriority { get; set; }
        bool IsSaveActivityHistory { get; set;  }
        bool IsSaveAttachments { get; set; }
        bool TriggerWorkflow { get; set; }
        bool IsSaveRequestData { get; set; }

        bool IsRequiredComment { get; set; }
        bool IsRequiredAttachment { get; set; }

    }
}
