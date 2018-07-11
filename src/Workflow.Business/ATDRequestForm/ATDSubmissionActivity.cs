using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.ATDRequestForm
{
    public class ATDSubmissionActivity : AbstractActivity<IATDFormDataProcessing>,IActivity<IATDFormDataProcessing>
    {

        IATDFormDataProcessing _FormDataProcessing = null;

        public ATDSubmissionActivity() : base(AbstractActivity<IATDFormDataProcessing>.FORM_SUBMISSION_ACTIVITY)
        {
            this.InitFormDataProcessing();
            this.AddAction(new DefaultAction<IATDFormDataProcessing>(AbstractAction<IATDFormDataProcessing>.SUBMITTED_ACTION, _FormDataProcessing));
        }

        private void InitFormDataProcessing()
        {
            _FormDataProcessing = new ATDFormDataProcessing()
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
