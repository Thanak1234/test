/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.ITRequestForm;

namespace Workflow.Business.FnFRequestForm
{
    public class FnFReviewActivity : AbstractActivity<IFnFFormDataProcessing>, IActivity<IFnFFormDataProcessing>
    {

        private IFnFFormDataProcessing _FnFFormDataProcessing = null;

        public FnFReviewActivity() : base(FnFRequestFormBC.FNF_REVIEW)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IFnFFormDataProcessing>(AbstractAction<IFnFFormDataProcessing>.REVIEWED_ACTION, _FnFFormDataProcessing));
            AddAction(new DefaultAction<IFnFFormDataProcessing>(AbstractAction<IFnFFormDataProcessing>.REWORKED_ACTION, _FnFFormDataProcessing));
            AddAction(new DefaultAction<IFnFFormDataProcessing>(AbstractAction<IFnFFormDataProcessing>.REJECTED_ACTION, _FnFFormDataProcessing));
        }
        private void InitFormDataProcessing()
        {
            _FnFFormDataProcessing = new FnFFormDataProcessing()
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
