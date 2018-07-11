using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.BcjRequestForm
{
    public class BcjDyCFOApprovalActivity : AbstractActivity<IBcjFormDataProcessing>, IActivity<IBcjFormDataProcessing>
    {
        private IBcjFormDataProcessing _bcjFormDataProcessing = null;

        public BcjDyCFOApprovalActivity() : base(BcjRequestFormBC.CFO_DyCFO)
        {
            InitFormDataProcessing();

            AddAction(new DefaultAction<IBcjFormDataProcessing>(AbstractAction<IBcjFormDataProcessing>.APPROVED_ACTION, _bcjFormDataProcessing));
            AddAction(new DefaultAction<IBcjFormDataProcessing>(AbstractAction<IBcjFormDataProcessing>.REJECTED_ACTION, _bcjFormDataProcessing));
            AddAction(new DefaultAction<IBcjFormDataProcessing>(AbstractAction<IBcjFormDataProcessing>.REWORKED_ACTION, _bcjFormDataProcessing));
        }

        private void InitFormDataProcessing()
        {
            _bcjFormDataProcessing = new BcjFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = false,
                IsSaveAttachments = true,
                //IsEditAttachments = false
            };
        }
    }
}
