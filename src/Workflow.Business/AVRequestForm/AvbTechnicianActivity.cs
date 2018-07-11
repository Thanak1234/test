/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.AVRequestForm
{
    public class AvbTechnicianActivity : AbstractActivity<IAvbFormDataProcessing>, IActivity<IAvbFormDataProcessing>
    {

        private IAvbFormDataProcessing _avbFormDataProcessing = null;

        public AvbTechnicianActivity() : base(AvbRequestFormBC.AV_TECH) {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IAvbFormDataProcessing>(AbstractAction<IAvbFormDataProcessing>.COMPLETED_ACTION, _avbFormDataProcessing));
            
        }

        private void InitFormDataProcessing()
        {
            _avbFormDataProcessing = new AvbFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = false,
                IsSaveAttachments = true
            };
        }

    }
}
