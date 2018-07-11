namespace Workflow.Business.BcjRequestForm
{
    public class Level2ApprovalAct : AbstractActivity<IBcjFormDataProcessing>, IActivity<IBcjFormDataProcessing>
    {
        private IBcjFormDataProcessing _processing = null;

        public Level2ApprovalAct() : base(BcjRequestFormBC.Level2Approval)
        {
            InitFormDataProcessing();

            AddAction(new DefaultAction<IBcjFormDataProcessing>(AbstractAction<IBcjFormDataProcessing>.APPROVED_ACTION, _processing));
            AddAction(new DefaultAction<IBcjFormDataProcessing>(AbstractAction<IBcjFormDataProcessing>.REJECTED_ACTION, _processing));
            AddAction(new DefaultAction<IBcjFormDataProcessing>(AbstractAction<IBcjFormDataProcessing>.REWORKED_ACTION, _processing));
        }

        private void InitFormDataProcessing()
        {
            _processing = new BcjFormDataProcessing()
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
