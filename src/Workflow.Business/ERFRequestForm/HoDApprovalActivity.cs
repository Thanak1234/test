/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.ERFRequestForm
{
    public class HoDApprovalActivity : AbstractActivity<IERFFormDataProcessing>, IActivity<IERFFormDataProcessing>
    {
        private IERFFormDataProcessing _ERFFormDataProcessing = null;

        public HoDApprovalActivity() : base(ERFRequestFormBC.DEPT_HOD_APPROVAL)
        {
            InitFormDataProcessing();
            
            AddAction(new DefaultAction<IERFFormDataProcessing>(AbstractAction<IERFFormDataProcessing>.APPROVED_ACTION, _ERFFormDataProcessing));
            AddAction(new DefaultAction<IERFFormDataProcessing>(AbstractAction<IERFFormDataProcessing>.REJECTED_ACTION, _ERFFormDataProcessing));
            AddAction(new DefaultAction<IERFFormDataProcessing>(AbstractAction<IERFFormDataProcessing>.REWORKED_ACTION, _ERFFormDataProcessing));
        }

        private void InitFormDataProcessing()
        {
            _ERFFormDataProcessing = new ERFFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = false,
                IsSaveAttachments = false
            };
        }
    }
}
