namespace Workflow.Business.FnFRequestForm
{
    public class FnFReworkedActivity : AbstractActivity<IFnFFormDataProcessing>, IActivity<IFnFFormDataProcessing>
    {

        private IFnFFormDataProcessing _fnfFormDataProcessing = null;
        private IFnFFormDataProcessing _fnfCancelFormDataProcessing = null;

        public FnFReworkedActivity() : base(FnFRequestFormBC.REWORKED)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IFnFFormDataProcessing>(AbstractAction<IFnFFormDataProcessing>.RE_SUBMITTED_ACTION, _fnfFormDataProcessing));
            AddAction(new DefaultAction<IFnFFormDataProcessing>(AbstractAction<IFnFFormDataProcessing>.CANCELED_ACTION, _fnfCancelFormDataProcessing));
        }

        private void InitFormDataProcessing ()
        {
            _fnfFormDataProcessing = new FnFFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = true,
                IsEditRequestor = true,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = true,
                IsSaveAttachments = true
            };

            _fnfCancelFormDataProcessing = new FnFFormDataProcessing()
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
