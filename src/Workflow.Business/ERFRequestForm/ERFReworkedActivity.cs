namespace Workflow.Business.ERFRequestForm
{
    public class ERFReworkedActivity : AbstractActivity<IERFFormDataProcessing>, IActivity<IERFFormDataProcessing>
    {

        private IERFFormDataProcessing _ERFFormDataProcessing = null;
        private IERFFormDataProcessing _ERFCancelFormDataProcessing = null;

        public ERFReworkedActivity() : base(ERFRequestFormBC.REWORKED)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IERFFormDataProcessing>(AbstractAction<IERFFormDataProcessing>.RE_SUBMITTED_ACTION, _ERFFormDataProcessing));
            AddAction(new DefaultAction<IERFFormDataProcessing>(AbstractAction<IERFFormDataProcessing>.CANCELED_ACTION, _ERFCancelFormDataProcessing));
        }

        private void InitFormDataProcessing ()
        {
            _ERFFormDataProcessing = new ERFFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = true,
                IsEditRequestor = true,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = true,
                IsSaveAttachments = true
            };

            _ERFCancelFormDataProcessing = new ERFFormDataProcessing()
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
