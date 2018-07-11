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
     public abstract class AbstractAction<T> : IAction<T> 
        where T : IFormDataProcessing
    {

        public const string SUBMITTED_ACTION = "Submitted";
        public const string RE_SUBMITTED_ACTION = "Resubmitted";
        public const string APPROVED_ACTION = "Approved";
        public const string REWORKED_ACTION = "Reworked";
        public const string REVIEWED_ACTION = "Reviewed";
        public const string REJECTED_ACTION = "Rejected";
        public const string CANCELED_ACTION = "Cancelled";
        public const string SAVE_ACTION = "Save Draft";
        public const string UPDATE_ACTION = "Update";
        public const string COMPLETED_ACTION = "Done";
        public const string EDITED_ACTION = "Edit";
        public const string AMENDED_ACTION = "Amended";
        
        public const string VERIFIED_ACTION = "Verified";
        public const string DISPOSED_ACTION = "Disposed";
        public const string UPDATED_ACTION = "Updated";

        private string _actionName = null;
        public string ActionName { get { return _actionName;  } }

        public abstract T FormDataProcessing { get; }

        public AbstractAction(string actionName)
        {
            _actionName = actionName;
        }

        public bool Match(IAction<T> action)
        {
            return ActionName == action.ActionName;
        }

       
    }
}
