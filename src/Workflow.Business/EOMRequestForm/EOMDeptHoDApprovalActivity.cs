/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.EOMRequestForm {
    public class EOMDeptHoDApprovalActivity : AbstractActivity<IEOMFormDataProcessing>, IActivity<IEOMFormDataProcessing>
    {
        private IEOMFormDataProcessing _EOMFormDataProcessing = null;

        public EOMDeptHoDApprovalActivity() : base(EOMRequestFormBC.DEPT_HOD_APPROVAL)
        {
            InitFormDataProcessing();
            
            AddAction(new DefaultAction<IEOMFormDataProcessing>(AbstractAction<IEOMFormDataProcessing>.APPROVED_ACTION, _EOMFormDataProcessing));
            AddAction(new DefaultAction<IEOMFormDataProcessing>(AbstractAction<IEOMFormDataProcessing>.REJECTED_ACTION, _EOMFormDataProcessing));
            AddAction(new DefaultAction<IEOMFormDataProcessing>(AbstractAction<IEOMFormDataProcessing>.REWORKED_ACTION, _EOMFormDataProcessing));
        }

        private void InitFormDataProcessing()
        {
            _EOMFormDataProcessing = new EOMFormDataProcessing()
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
