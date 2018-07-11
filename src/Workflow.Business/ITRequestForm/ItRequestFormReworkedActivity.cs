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
    public class ItRequestFormReworkedActivity : AbstractActivity<IItFormDataProcessing>, IActivity<IItFormDataProcessing>
    {

        private IItFormDataProcessing _formDataProcessing = null;
        private IItFormDataProcessing _cancellformDataProcessing = null;

    
        public ItRequestFormReworkedActivity() : base(ItRequestFormBC.REWORKED) {
            InitFormDataProcessing();
           
            AddAction(new DefaultAction<IItFormDataProcessing>(AbstractAction<IItFormDataProcessing>.RE_SUBMITTED_ACTION, _formDataProcessing));
            AddAction(new DefaultAction<IItFormDataProcessing>(AbstractAction<IItFormDataProcessing>.CANCELED_ACTION, _cancellformDataProcessing));

        }

       

        private void InitFormDataProcessing()
        {
            _formDataProcessing = new ItFromDataProcessing() {
                IsAddNewRequestHeader = false,
                IsEditPriority = true,
                IsEditRequestor = true,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = true,
                IsSaveAttachments = true
            };

            _cancellformDataProcessing = new  ItFromDataProcessing()
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
