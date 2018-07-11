using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.BcjRequestForm
{
    public class BcjReworkedActivity : AbstractActivity<IBcjFormDataProcessing>, IActivity<IBcjFormDataProcessing>
    {

        private IBcjFormDataProcessing _BcjFormDataProcessing = null;
        private IBcjFormDataProcessing _BcjCancelFormDataProcessing = null;

        public BcjReworkedActivity() : base(BcjRequestFormBC.REWORKED)
        {
            InitFormDataProcessing();

            AddAction(new DefaultAction<IBcjFormDataProcessing>(AbstractAction<IBcjFormDataProcessing>.RE_SUBMITTED_ACTION, _BcjFormDataProcessing));
            AddAction(new DefaultAction<IBcjFormDataProcessing>(AbstractAction<IBcjFormDataProcessing>.CANCELED_ACTION, _BcjCancelFormDataProcessing));
        }
        private void InitFormDataProcessing ()
        {
            _BcjFormDataProcessing = new BcjFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = true,
                IsEditRequestor = true,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = true,
                IsSaveAttachments = true,
                //IsEditAttachments = true
            };

            _BcjCancelFormDataProcessing = new BcjFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = false,
                IsSaveAttachments = false,
                //IsEditAttachments = false
            };

        }
    }
}
