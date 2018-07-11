/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.AvdrForm;

namespace Workflow.Business.AvdrForm {
    public class AvdrFormSubmissionActivity : AbstractActivity<IAvdrFormDataProcessing>, IActivity<IAvdrFormDataProcessing>
    {

        private IAvdrFormDataProcessing _FormDataProcessing = null;

    
        public AvdrFormSubmissionActivity() : base(AvdrFormBC.FORM_SUBMISSION) {
            InitFormDataProcessing();
           
            AddAction(new DefaultAction<IAvdrFormDataProcessing>(AbstractAction<IAvdrFormDataProcessing>.SUBMITTED_ACTION, _FormDataProcessing));

        }

       

        private void InitFormDataProcessing()
        {
            _FormDataProcessing = new AvdrFormDataProcessing() {
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
