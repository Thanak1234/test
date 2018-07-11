namespace Workflow.Business.BcjRequestForm
{
    public class BcjDraftActivity : AbstractActivity<IBcjFormDataProcessing>, IActivity<IBcjFormDataProcessing>
    {
        public BcjDraftActivity() : base(FORM_DRAFT_ACTIVITY)
        {
            AddAction(new DefaultAction<IBcjFormDataProcessing>(
                AbstractAction<IBcjFormDataProcessing>.SUBMITTED_ACTION, 
                new BcjFormDataProcessing()
                {

                    IsAddNewRequestHeader = false,
                    IsEditPriority = false,
                    IsEditRequestor = true,
                    IsSaveActivityHistory = false,
                    IsUpdateLastActivity = true,
                    IsSaveRequestData = true,
                    IsSaveAttachments = true
                })
            );
            AddAction(new DefaultAction<IBcjFormDataProcessing>(
                AbstractAction<IBcjFormDataProcessing>.SAVE_ACTION,
                new BcjFormDataProcessing()
                {
                    IsAddNewRequestHeader = false,
                    IsEditPriority = false,
                    IsEditRequestor = true,
                    IsSaveActivityHistory = false,
                    IsUpdateLastActivity = true,
                    IsSaveRequestData = true,
                    IsSaveAttachments = true,
                    TriggerWorkflow = false
                })
            );
        }
    }
}
