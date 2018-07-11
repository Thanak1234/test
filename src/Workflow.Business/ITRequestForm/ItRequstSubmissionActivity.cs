/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.ITRequestForm
{
    public class ItRequstSubmissionActivity : AbstractActivity<IItFormDataProcessing> , IActivity<IItFormDataProcessing> 
    {
       
        private IItFormDataProcessing _itFormDataProcessing;

        public ItRequstSubmissionActivity() :base(AbstractActivity<IItFormDataProcessing>.FORM_SUBMISSION_ACTIVITY)
        {

            InitFormDataProcessing();
            AddAction(new DefaultAction<IItFormDataProcessing>(AbstractAction<IItFormDataProcessing>.SUBMITTED_ACTION, _itFormDataProcessing));
            AddAction(new DefaultAction<IItFormDataProcessing>(AbstractAction<IItFormDataProcessing>.SAVE_ACTION, _itFormDataProcessing));
        }

        private void InitFormDataProcessing()
        {
            _itFormDataProcessing = new ItFromDataProcessing()
            {

                IsAddNewRequestHeader = true,
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
