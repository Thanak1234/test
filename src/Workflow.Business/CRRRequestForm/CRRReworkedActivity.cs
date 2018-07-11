namespace Workflow.Business.CRRRequestForm
{
    public class CRRReworkedActivity : AbstractActivity<ICRRFormDataProcessing>, IActivity<ICRRFormDataProcessing>
    {

        private ICRRFormDataProcessing _CRRFormDataProcessing = null;
        private ICRRFormDataProcessing _CRRCancelFormDataProcessing = null;

        public CRRReworkedActivity() : base(CRRRequestFormBC.REWORKED)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<ICRRFormDataProcessing>(AbstractAction<ICRRFormDataProcessing>.RE_SUBMITTED_ACTION, _CRRFormDataProcessing));
            AddAction(new DefaultAction<ICRRFormDataProcessing>(AbstractAction<ICRRFormDataProcessing>.CANCELED_ACTION, _CRRCancelFormDataProcessing));
        }

        private void InitFormDataProcessing ()
        {
            _CRRFormDataProcessing = new CRRFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = true,
                IsEditRequestor = true,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = true,
                IsSaveAttachments = true
            };

            _CRRCancelFormDataProcessing = new CRRFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = false,
                IsSaveAttachments = false
            };
        }
    }
}
