namespace Workflow.Business.FnFRequestForm
{
    public class FnFSubmissionActivity : AbstractActivity<IFnFFormDataProcessing>, IActivity<IFnFFormDataProcessing>
    {

        private IFnFFormDataProcessing _FnFFormDataProcessing = null;
        public FnFSubmissionActivity() : base(AbstractActivity<IFnFFormDataProcessing>.FORM_SUBMISSION_ACTIVITY)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IFnFFormDataProcessing>(AbstractAction<IFnFFormDataProcessing>.SUBMITTED_ACTION, _FnFFormDataProcessing));             
        }
        
        private void InitFormDataProcessing()
        {
            _FnFFormDataProcessing = new FnFFormDataProcessing()
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
