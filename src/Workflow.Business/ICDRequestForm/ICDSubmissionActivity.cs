using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.ICDRequestForm
{
    public class ICDSubmissionActivity : AbstractActivity<IICDFormDataProcessing>,IActivity<IICDFormDataProcessing>
    {

        IICDFormDataProcessing _formDataProcessing = null;

        public ICDSubmissionActivity() :base(AbstractActivity<IICDFormDataProcessing>.FORM_SUBMISSION_ACTIVITY)
        {
            this.InitFormDataProcessing();
            AddAction(new DefaultAction<IICDFormDataProcessing>(AbstractAction<IICDFormDataProcessing>.SUBMITTED_ACTION, _formDataProcessing));
        }

        private void InitFormDataProcessing()
        {
            _formDataProcessing = new ICDFormDataProcessing()
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
