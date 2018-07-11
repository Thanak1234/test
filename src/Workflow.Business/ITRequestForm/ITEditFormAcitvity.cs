using Workflow.MSExchange;
using System;

namespace Workflow.Business.ITRequestForm
{
    public class ITEditFormAcitvity : AbstractActivity<IItFormDataProcessing>, IActivity<IItFormDataProcessing>
    {

        private IItFormDataProcessing _ATTFormDataProcessing = null;
        private IItFormDataProcessing _ATTFormCancelDataProcessing = null;

        private Func<IEmailData> _GetEmailData;

        public ITEditFormAcitvity(Func<IEmailData> GetEmailData) : base(ItRequestFormBC.EDIT)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IItFormDataProcessing>(AbstractAction<IItFormDataProcessing>.EDITED_ACTION, _ATTFormDataProcessing));
            AddAction(new DefaultAction<IItFormDataProcessing>(AbstractAction<IItFormDataProcessing>.CANCELED_ACTION, _ATTFormCancelDataProcessing));
            _GetEmailData = GetEmailData;
        }

        public override IEmailData Email { get { return _GetEmailData() ; } }

        private void InitFormDataProcessing()
        {
            _ATTFormDataProcessing = new ItFromDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = true,
                IsUpdateLastActivity = false,
                IsSaveRequestData = true,
                IsSaveAttachments = true,
                TriggerWorkflow = false
            };

            _ATTFormCancelDataProcessing = new ItFromDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = true,
                IsUpdateLastActivity = true,
                IsSaveRequestData = true,
                IsSaveAttachments = true,
                TriggerWorkflow = false
            };
        }

    }
}
