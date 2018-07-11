/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.EOMRequestForm {
    public class EOMRequestSubmissionActivity : AbstractActivity<IEOMFormDataProcessing>, IActivity<IEOMFormDataProcessing>
    {

        private IEOMFormDataProcessing _EOMFormDataProcessing = null;
        public EOMRequestSubmissionActivity() : base(AbstractActivity<IEOMFormDataProcessing>.FORM_SUBMISSION_ACTIVITY)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IEOMFormDataProcessing>(AbstractAction<IEOMFormDataProcessing>.SUBMITTED_ACTION, _EOMFormDataProcessing));             
        }


        private void InitFormDataProcessing()
        {
            _EOMFormDataProcessing = new EOMFormDataProcessing()
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
