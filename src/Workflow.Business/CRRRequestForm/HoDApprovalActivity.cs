/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.CRRRequestForm
{
    public class HoDApprovalActivity : AbstractActivity<ICRRFormDataProcessing>, IActivity<ICRRFormDataProcessing>
    {
        private ICRRFormDataProcessing _CRRFormDataProcessing = null;

        public HoDApprovalActivity() : base(CRRRequestFormBC.DEPT_HOD_APPROVAL)
        {
            InitFormDataProcessing();
            
            AddAction(new DefaultAction<ICRRFormDataProcessing>(AbstractAction<ICRRFormDataProcessing>.APPROVED_ACTION, _CRRFormDataProcessing));
            AddAction(new DefaultAction<ICRRFormDataProcessing>(AbstractAction<ICRRFormDataProcessing>.REJECTED_ACTION, _CRRFormDataProcessing));
            AddAction(new DefaultAction<ICRRFormDataProcessing>(AbstractAction<ICRRFormDataProcessing>.REWORKED_ACTION, _CRRFormDataProcessing));
        }

        private void InitFormDataProcessing()
        {
            _CRRFormDataProcessing = new CRRFormDataProcessing()
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
