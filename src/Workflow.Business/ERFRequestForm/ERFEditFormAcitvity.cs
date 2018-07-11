using Workflow.MSExchange;
using System;

namespace Workflow.Business.ERFRequestForm
{
    public class ERFEditFormAcitvity : AbstractActivity<IERFFormDataProcessing>, IActivity<IERFFormDataProcessing>
    {

        private IERFFormDataProcessing _ERFFormDataProcessing = null;
        private IERFFormDataProcessing _ERFFormCancelDataProcessing = null;

        private Func<IEmailData> _GetEmailData;

        public ERFEditFormAcitvity(Func<IEmailData> GetEmailData) : base(ERFRequestFormBC.EDIT)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IERFFormDataProcessing>(AbstractAction<IERFFormDataProcessing>.EDITED_ACTION, _ERFFormDataProcessing));
            AddAction(new DefaultAction<IERFFormDataProcessing>(AbstractAction<IERFFormDataProcessing>.CANCELED_ACTION, _ERFFormCancelDataProcessing));
            _GetEmailData = GetEmailData;
            //Email configuration
        }

        public override IEmailData Email { get { return _GetEmailData() ; } }

        private void InitFormDataProcessing()
        {
            _ERFFormDataProcessing = new ERFFormDataProcessing()
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

            _ERFFormCancelDataProcessing = new ERFFormDataProcessing()
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
