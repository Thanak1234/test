namespace Workflow.Business.BcjRequestForm
{
    public class BcjPurchasingActivity : AbstractActivity<IBcjFormDataProcessing>, IActivity<IBcjFormDataProcessing>
    {
        private IBcjFormDataProcessing _bcjFormDataProcessing = null;

        public BcjPurchasingActivity() : base(BcjRequestFormBC.Purchasing)
        {
            InitFormDataProcessing();

            AddAction(new DefaultAction<IBcjFormDataProcessing>(AbstractAction<IBcjFormDataProcessing>.COMPLETED_ACTION, _bcjFormDataProcessing));
        }

        private void InitFormDataProcessing()
        {
            _bcjFormDataProcessing = new BcjFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = true,
                IsSaveAttachments = true
            };
        }
    }
}
