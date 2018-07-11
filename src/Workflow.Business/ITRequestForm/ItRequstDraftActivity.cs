/**
*@author : Phanny
*/

namespace Workflow.Business.ITRequestForm
{
    public class ItRequstDraftActivity : AbstractActivity<IItFormDataProcessing> , IActivity<IItFormDataProcessing> 
    {
        public ItRequstDraftActivity() :base(FORM_DRAFT_ACTIVITY)
        {
            AddAction(new DefaultAction<IItFormDataProcessing>(
                AbstractAction<IItFormDataProcessing>.SUBMITTED_ACTION, new ItFromDataProcessing()
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
            AddAction(new DefaultAction<IItFormDataProcessing>(
                AbstractAction<IItFormDataProcessing>.SAVE_ACTION, 
                new ItFromDataProcessing()
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
