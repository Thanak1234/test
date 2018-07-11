/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.AvdrForm
{
    public class AvdrFormEditActivity : AbstractActivity<IAvdrFormDataProcessing>, IActivity<IAvdrFormDataProcessing>
    {

        private IAvdrFormDataProcessing _formDataProcessing = null;
    
        public AvdrFormEditActivity() : base(AvdrFormBC.EDIT) {
            InitFormDataProcessing();
           
            AddAction(new DefaultAction<IAvdrFormDataProcessing>(AbstractAction<IAvdrFormDataProcessing>.EDITED_ACTION, _formDataProcessing));

        }

       

        private void InitFormDataProcessing()
        {
            _formDataProcessing = new AvdrFormDataProcessing() {
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
