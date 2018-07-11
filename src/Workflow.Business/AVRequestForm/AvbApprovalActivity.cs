/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.ITRequestForm;

namespace Workflow.Business.AVRequestForm
{
    public class AvbApprovalActivity : AbstractActivity<IAvbFormDataProcessing>, IActivity<IAvbFormDataProcessing>
    {

        private IAvbFormDataProcessing _avbFormDataProcessing = null;

        public AvbApprovalActivity() : base(AvbRequestFormBC.AV_APPROVAL)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IAvbFormDataProcessing>(AbstractAction<IAvbFormDataProcessing>.APPROVED_ACTION, _avbFormDataProcessing));
            AddAction(new DefaultAction<IAvbFormDataProcessing>(AbstractAction<IAvbFormDataProcessing>.REWORKED_ACTION, _avbFormDataProcessing));
            AddAction(new DefaultAction<IAvbFormDataProcessing>(AbstractAction<IAvbFormDataProcessing>.REJECTED_ACTION, _avbFormDataProcessing));
        }
        private void InitFormDataProcessing()
        {
            _avbFormDataProcessing = new AvbFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = false,
                IsSaveAttachments = true
            };
        }
    }
}
