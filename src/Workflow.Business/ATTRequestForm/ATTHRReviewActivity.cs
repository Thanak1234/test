namespace Workflow.Business.ATTRequestForm
{
    public class ATTHRReviewActivity : AbstractActivity<IATTFormDataProcessing>, IActivity<IATTFormDataProcessing>
    {

        private IATTFormDataProcessing _ATTFormDataProcessing = null;
        public ATTHRReviewActivity() : base(ATTRequestFormBC.HR_REVIEW)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.REJECTED_ACTION, _ATTFormDataProcessing));
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.REWORKED_ACTION, _ATTFormDataProcessing));
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.REVIEWED_ACTION, _ATTFormDataProcessing));
        }
        
        private void InitFormDataProcessing()
        {
            _ATTFormDataProcessing = new ATTFormDataProcessing()
            {
                IsSaveAttachments = true,
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsSaveRequestData = false,
                IsUpdateLastActivity = false 
            };
        }
    }
}
