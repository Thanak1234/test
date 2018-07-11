/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.AvirForm {
    public class AvirFormSubmissionActivity : AbstractActivity<IAvirFormDataProcessing>, IActivity<IAvirFormDataProcessing>
    {

        private IAvirFormDataProcessing _FormDataProcessing = null;

    
        public AvirFormSubmissionActivity() : base(AvirFormBC.FORM_SUBMISSION) {
            InitFormDataProcessing();
           
            AddAction(new DefaultAction<IAvirFormDataProcessing>(AbstractAction<IAvirFormDataProcessing>.SUBMITTED_ACTION, _FormDataProcessing));

        }

       

        private void InitFormDataProcessing()
        {
            _FormDataProcessing = new AvirFormDataProcessing() {
                TriggerWorkflow = false,
                IsSaveAttachments = false,
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsSaveRequestData = true,
                IsSaveBusinessData = true,
                IsUpdateLastActivity = false
            };
        }

       

    }
}
