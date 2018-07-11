using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.AVRequestForm;

namespace Workflow.Business.BcjRequestForm
{
    public class BcjRequestSubmissionActivity : AbstractActivity<IBcjFormDataProcessing>, IActivity<IBcjFormDataProcessing>
    {

        private IBcjFormDataProcessing _bcjFormDataProcessing = null;
        public BcjRequestSubmissionActivity() : base(AbstractActivity<IBcjFormDataProcessing>.FORM_SUBMISSION_ACTIVITY)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IBcjFormDataProcessing>(AbstractAction<IBcjFormDataProcessing>.SUBMITTED_ACTION, _bcjFormDataProcessing));
            AddAction(new DefaultAction<IBcjFormDataProcessing>(AbstractAction<IBcjFormDataProcessing>.SAVE_ACTION, _bcjFormDataProcessing));
        }


        private void InitFormDataProcessing()
        {
            _bcjFormDataProcessing = new BcjFormDataProcessing()
            {
                IsSaveAttachments = true,
                IsAddNewRequestHeader = true,
                //IsEditAttachments = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsSaveRequestData = true,
                IsUpdateLastActivity = false
                
            };
        }
    }
}
