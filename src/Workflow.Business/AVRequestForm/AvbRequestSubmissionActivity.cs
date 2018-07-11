/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.AVRequestForm
{
    public class AvbRequestSubmissionActivity : AbstractActivity<IAvbFormDataProcessing>, IActivity<IAvbFormDataProcessing>
    {

        private IAvbFormDataProcessing _avbFormDataProcessing = null;
        public AvbRequestSubmissionActivity() : base(AbstractActivity<IAvbFormDataProcessing>.FORM_SUBMISSION_ACTIVITY)
        {
            //AddAction(submittedAction);
            InitFormDataProcessing();
            AddAction(new DefaultAction<IAvbFormDataProcessing>(AbstractAction<IAvbFormDataProcessing>.SUBMITTED_ACTION, _avbFormDataProcessing));             
        }


        private void InitFormDataProcessing()
        {
            _avbFormDataProcessing = new AvbFormDataProcessing()
            {
                IsSaveAttachments = true,
                IsAddNewRequestHeader = true,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsSaveRequestData = true,
                IsUpdateLastActivity = false 
            };
        }

    }
}
