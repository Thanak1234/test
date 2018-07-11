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
    public class FnFApprovalActivity : AbstractActivity<IFnFFormDataProcessing>, IActivity<IFnFFormDataProcessing>
    {

        private IFnFFormDataProcessing _FnFFormDataProcessing = null;

        public FnFApprovalActivity() : base(FnFRequestFormBC.FNF_APPROVAL)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IFnFFormDataProcessing>(AbstractAction<IFnFFormDataProcessing>.APPROVED_ACTION, _FnFFormDataProcessing));
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
