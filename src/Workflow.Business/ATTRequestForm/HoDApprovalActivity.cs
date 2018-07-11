/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.ATTRequestForm
{
    public class HoDApprovalActivity : AbstractActivity<IATTFormDataProcessing>, IActivity<IATTFormDataProcessing>
    {
        private IATTFormDataProcessing _ATTFormDataProcessing = null;

        public HoDApprovalActivity() : base(ATTRequestFormBC.DEPT_HOD_APPROVAL)
        {
            InitFormDataProcessing();
            
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.APPROVED_ACTION, _ATTFormDataProcessing));
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.REJECTED_ACTION, _ATTFormDataProcessing));
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.REWORKED_ACTION, _ATTFormDataProcessing));
        }

        private void InitFormDataProcessing()
        {
            _ATTFormDataProcessing = new ATTFormDataProcessing()
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
