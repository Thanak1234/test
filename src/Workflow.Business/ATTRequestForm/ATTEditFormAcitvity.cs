using Workflow.MSExchange;
using System;

namespace Workflow.Business.ATTRequestForm
{
    public class ATTEditFormAcitvity : AbstractActivity<IATTFormDataProcessing>, IActivity<IATTFormDataProcessing>
    {

        private IATTFormDataProcessing _ATTFormDataProcessing = null;
        private IATTFormDataProcessing _ATTFormCancelDataProcessing = null;

        private Func<IEmailData> _GetEmailData;

        public ATTEditFormAcitvity(Func<IEmailData> GetEmailData) : base(ATTRequestFormBC.EDIT)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.EDITED_ACTION, _ATTFormDataProcessing));
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.CANCELED_ACTION, _ATTFormCancelDataProcessing));
            _GetEmailData = GetEmailData;
            //Email configuration
        }

        public override IEmailData Email { get { return _GetEmailData() ; } }

        private void InitFormDataProcessing()
        {
            _ATTFormDataProcessing = new ATTFormDataProcessing()
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

            _ATTFormCancelDataProcessing = new ATTFormDataProcessing()
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
