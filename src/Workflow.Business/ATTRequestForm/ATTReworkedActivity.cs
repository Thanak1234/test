namespace Workflow.Business.ATTRequestForm
{
    public class ATTReworkedActivity : AbstractActivity<IATTFormDataProcessing>, IActivity<IATTFormDataProcessing>
    {

        private IATTFormDataProcessing _ATTFormDataProcessing = null;
        private IATTFormDataProcessing _ATTCancelFormDataProcessing = null;

        public ATTReworkedActivity() : base(ATTRequestFormBC.REWORKED)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.RE_SUBMITTED_ACTION, _ATTFormDataProcessing));
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.CANCELED_ACTION, _ATTCancelFormDataProcessing));
        }

        private void InitFormDataProcessing ()
        {
            _ATTFormDataProcessing = new ATTFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = true,
                IsEditRequestor = true,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = true,
                IsSaveAttachments = true
            };

            _ATTCancelFormDataProcessing = new ATTFormDataProcessing()
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
