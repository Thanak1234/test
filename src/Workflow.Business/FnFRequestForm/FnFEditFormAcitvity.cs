using Workflow.MSExchange;
using System;

namespace Workflow.Business.FnFRequestForm
{
    public class FnFEditFormAcitvity : AbstractActivity<IFnFFormDataProcessing>, IActivity<IFnFFormDataProcessing>
    {

        private IFnFFormDataProcessing _FnFFormDataProcessing = null;
        private IFnFFormDataProcessing _FnFFormCancelDataProcessing = null;

        private Func<IEmailData> _GetEmailData;

        public FnFEditFormAcitvity(Func<IEmailData> GetEmailData) : base(FnFRequestFormBC.EDIT)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IFnFFormDataProcessing>(AbstractAction<IFnFFormDataProcessing>.EDITED_ACTION, _FnFFormDataProcessing));
            AddAction(new DefaultAction<IFnFFormDataProcessing>(AbstractAction<IFnFFormDataProcessing>.CANCELED_ACTION, _FnFFormCancelDataProcessing));
            _GetEmailData = GetEmailData;
            //Email configuration
        }

        public override IEmailData Email { get { return _GetEmailData() ; } }

        private void InitFormDataProcessing()
        {
            _FnFFormDataProcessing = new FnFFormDataProcessing()
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

            _FnFFormCancelDataProcessing = new FnFFormDataProcessing()
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
