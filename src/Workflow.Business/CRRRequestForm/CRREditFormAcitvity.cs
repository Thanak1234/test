using Workflow.MSExchange;
using System;





namespace Workflow.Business.CRRRequestForm
{
    public class CRREditFormAcitvity : AbstractActivity<ICRRFormDataProcessing>, IActivity<ICRRFormDataProcessing>
    {

        private ICRRFormDataProcessing _CRRFormDataProcessing = null;
        private ICRRFormDataProcessing _CRRFormCancelDataProcessing = null;

        private Func<IEmailData> _GetEmailData;

        public CRREditFormAcitvity(Func<IEmailData> GetEmailData) : base(CRRRequestFormBC.EDIT)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<ICRRFormDataProcessing>(AbstractAction<ICRRFormDataProcessing>.EDITED_ACTION, _CRRFormDataProcessing));  
            AddAction(new DefaultAction<ICRRFormDataProcessing>(AbstractAction<ICRRFormDataProcessing>.CANCELED_ACTION, _CRRFormCancelDataProcessing));
            _GetEmailData = GetEmailData;
            //Email configuration
        }

        public override IEmailData Email { get { return _GetEmailData() ; } }

        private void InitFormDataProcessing()
        {
            _CRRFormDataProcessing = new CRRFormDataProcessing()
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

            _CRRFormCancelDataProcessing = new CRRFormDataProcessing()
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
