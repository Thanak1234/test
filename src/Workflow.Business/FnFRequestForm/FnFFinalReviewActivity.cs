/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.FnFRequestForm
{
    public class FnFFinalReviewActivity : AbstractActivity<IFnFFormDataProcessing>, IActivity<IFnFFormDataProcessing>
    {

        private IFnFFormDataProcessing _fnfFormDataProcessing = null;

        public FnFFinalReviewActivity() : base(FnFRequestFormBC.FNF_FINAL_REVIEW) {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IFnFFormDataProcessing>(AbstractAction<IFnFFormDataProcessing>.COMPLETED_ACTION, _fnfFormDataProcessing));
            
        }

        private void InitFormDataProcessing()
        {
            _fnfFormDataProcessing = new FnFFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = true,
                IsSaveAttachments = true
            };
        }

    }
}
