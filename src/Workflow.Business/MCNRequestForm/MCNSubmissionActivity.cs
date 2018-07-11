using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.MCNRequestForm
{

    public class MCNSubmissionActivity : AbstractActivity<IMCNFormDataProcessing>,IActivity<IMCNFormDataProcessing>
    {

        IMCNFormDataProcessing _formDataProcessing = null;

        public MCNSubmissionActivity() : base(AbstractActivity<IMCNFormDataProcessing>.FORM_SUBMISSION_ACTIVITY)
        {
            this.initFormDataProcessing();
            AddAction(new DefaultAction<IMCNFormDataProcessing>(AbstractAction<IMCNFormDataProcessing>.SUBMITTED_ACTION, _formDataProcessing));
        }

        private void initFormDataProcessing()
        {
            _formDataProcessing = new MCNFormDataProcessing()
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
