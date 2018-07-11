namespace Workflow.Business.CRRRequestForm
{
    public class CRRSubmissionActivity : AbstractActivity<ICRRFormDataProcessing>, IActivity<ICRRFormDataProcessing>
    {

        private ICRRFormDataProcessing _CRRFormDataProcessing = null;
        public CRRSubmissionActivity() : base(AbstractActivity<ICRRFormDataProcessing>.FORM_SUBMISSION_ACTIVITY)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<ICRRFormDataProcessing>(AbstractAction<ICRRFormDataProcessing>.SUBMITTED_ACTION, _CRRFormDataProcessing));             
        }
        
        private void InitFormDataProcessing()
        {
            _CRRFormDataProcessing = new CRRFormDataProcessing()
            {
                IsSaveAttachments = true,
                IsAddNewRequestHeader = true,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsSaveRequestData = true,
                IsUpdateLastActivity = false 
            };
        }
    }
}
