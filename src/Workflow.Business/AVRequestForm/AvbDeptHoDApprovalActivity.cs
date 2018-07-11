/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.AVRequestForm
{
    public class AvbDeptHoDApprovalActivity : AbstractActivity<IAvbFormDataProcessing>, IActivity<IAvbFormDataProcessing>
    {
        private IAvbFormDataProcessing _avbFormDataProcessing = null;

        public AvbDeptHoDApprovalActivity() : base(AvbRequestFormBC.DEPT_HOD_APPROVAL)
        {
            InitFormDataProcessing();
            
            AddAction(new DefaultAction<IAvbFormDataProcessing>(AbstractAction<IAvbFormDataProcessing>.APPROVED_ACTION, _avbFormDataProcessing));
            AddAction(new DefaultAction<IAvbFormDataProcessing>(AbstractAction<IAvbFormDataProcessing>.REJECTED_ACTION, _avbFormDataProcessing));
            AddAction(new DefaultAction<IAvbFormDataProcessing>(AbstractAction<IAvbFormDataProcessing>.REWORKED_ACTION, _avbFormDataProcessing));
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
