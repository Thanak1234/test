/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.AvirForm {
    public class AvirFormEditActivity : AbstractActivity<IAvirFormDataProcessing>, IActivity<IAvirFormDataProcessing>
    {

        private IAvirFormDataProcessing _formDataProcessing = null;
    
        public AvirFormEditActivity() : base(AvirFormBC.EDIT) {
            InitFormDataProcessing();
           
            AddAction(new DefaultAction<IAvirFormDataProcessing>(AbstractAction<IAvirFormDataProcessing>.EDITED_ACTION, _formDataProcessing));

        }

       

        private void InitFormDataProcessing()
        {
            _formDataProcessing = new AvirFormDataProcessing() {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = true,
                IsSaveBusinessData = true,
                IsSaveAttachments = false,
                TriggerWorkflow = false
            };
        }

       

    }
}
