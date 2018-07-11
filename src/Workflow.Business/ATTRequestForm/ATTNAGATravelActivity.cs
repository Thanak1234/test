namespace Workflow.Business.ATTRequestForm
{
    public class ATTNAGATravelActivity : AbstractActivity<IATTFormDataProcessing>, IActivity<IATTFormDataProcessing>
    {

        private IATTFormDataProcessing _ATTFormDataProcessing = null;
        public ATTNAGATravelActivity() : base(ATTRequestFormBC.NAGA_TRAVEL)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IATTFormDataProcessing>(AbstractAction<IATTFormDataProcessing>.COMPLETED_ACTION, _ATTFormDataProcessing));
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
                IsSaveRequestData = true,
                IsUpdateLastActivity = false 
            };
        }
    }
}
