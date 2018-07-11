/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.ITRequestForm
{
    public class DeptHoDApprovalActivity : AbstractActivity<IItFormDataProcessing>, IActivity<IItFormDataProcessing>
    {


        private IItFormDataProcessing _itFormDataProcessing;
        private IItFormDataProcessing _itFormRejectDataProcessing;


        public DeptHoDApprovalActivity() : base(ItRequestFormBC.DEPT_HOD_APPROVAL)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IItFormDataProcessing>(AbstractAction<IItFormDataProcessing>.APPROVED_ACTION, _itFormDataProcessing));
            AddAction(new DefaultAction<IItFormDataProcessing>(AbstractAction<IItFormDataProcessing>.REWORKED_ACTION, _itFormDataProcessing));
            AddAction(new DefaultAction<IItFormDataProcessing>(AbstractAction<IItFormDataProcessing>.REJECTED_ACTION, _itFormDataProcessing));

           
        }

        private void InitFormDataProcessing()
        {
            _itFormDataProcessing = new ItFromDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = false,
                IsSaveAttachments = true,
            };

            _itFormRejectDataProcessing = new ItFromDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = false,
                IsSaveAttachments = false,
            };
        }

    }
}
