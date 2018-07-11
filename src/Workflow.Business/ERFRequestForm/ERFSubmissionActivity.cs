namespace Workflow.Business.ERFRequestForm
{
    public class ERFSubmissionActivity : AbstractActivity<IERFFormDataProcessing>, IActivity<IERFFormDataProcessing>
    {

        private IERFFormDataProcessing _ERFFormDataProcessing = null;
        public ERFSubmissionActivity() : base(AbstractActivity<IERFFormDataProcessing>.FORM_SUBMISSION_ACTIVITY)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IERFFormDataProcessing>(AbstractAction<IERFFormDataProcessing>.SUBMITTED_ACTION, _ERFFormDataProcessing));             
        }
        
        private void InitFormDataProcessing()
        {
            _ERFFormDataProcessing = new ERFFormDataProcessing()
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
