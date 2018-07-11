using Workflow.MSExchange;
using System;
using Workflow.Business.BcjRequestForm;

namespace Workflow.Business.BcjRequestForm
{
    public class BcjEditFormAcitvity : AbstractActivity<IBcjFormDataProcessing>, IActivity<IBcjFormDataProcessing>
    {

        private IBcjFormDataProcessing _BcjFormDataProcessing = null;
        private IBcjFormDataProcessing _PBFFormCancelDataProcessing = null;

        private Func<IEmailData> _GetEmailData;

        public BcjEditFormAcitvity(Func<IEmailData> GetEmailData) : base(BcjRequestFormBC.EDIT)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IBcjFormDataProcessing>(AbstractAction<IBcjFormDataProcessing>.EDITED_ACTION, _BcjFormDataProcessing));
            AddAction(new DefaultAction<IBcjFormDataProcessing>(AbstractAction<IBcjFormDataProcessing>.CANCELED_ACTION, _PBFFormCancelDataProcessing));
            _GetEmailData = GetEmailData;
            //Email configuration
        }

        public override IEmailData Email { get { return _GetEmailData() ; } }

        private void InitFormDataProcessing()
        {
            _BcjFormDataProcessing = new BcjFormDataProcessing()
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

            _PBFFormCancelDataProcessing = new BcjFormDataProcessing()
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
