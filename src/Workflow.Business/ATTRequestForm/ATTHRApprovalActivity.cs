namespace Workflow.Business.ATTRequestForm
{
    public class ATTHRApprovalActivity : AbstractActivity<IATTFormDataProcessing>, IActivity<IATTFormDataProcessing>
    {

        private IATTFormDataProcessing _ATTFormDataProcessing = null;
        public ATTHRApprovalActivity() : base(ATTRequestFormBC.HR_APPROVAL)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.APPROVED_ACTION, _ATTFormDataProcessing));
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.REJECTED_ACTION, _ATTFormDataProcessing));
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.REWORKED_ACTION, _ATTFormDataProcessing));            
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
