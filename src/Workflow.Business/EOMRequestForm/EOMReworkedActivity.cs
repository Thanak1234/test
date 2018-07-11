/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.EOMRequestForm {
    public class EOMReworkedActivity : AbstractActivity<IEOMFormDataProcessing>, IActivity<IEOMFormDataProcessing>
    {

        private IEOMFormDataProcessing _EOMFormDataProcessing = null;
        private IEOMFormDataProcessing _EOMCancelFormDataProcessing = null;

        public EOMReworkedActivity() : base(EOMRequestFormBC.REWORKED)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IEOMFormDataProcessing>(AbstractAction<IEOMFormDataProcessing>.RE_SUBMITTED_ACTION, _EOMFormDataProcessing));
            AddAction(new DefaultAction<IEOMFormDataProcessing>(AbstractAction<IEOMFormDataProcessing>.CANCELED_ACTION, _EOMCancelFormDataProcessing));
        }
        private void InitFormDataProcessing ()
        {
            _EOMFormDataProcessing = new EOMFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = true,
                IsEditRequestor = true,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = true,
                IsSaveAttachments = true
            };

            _EOMCancelFormDataProcessing = new EOMFormDataProcessing()
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
