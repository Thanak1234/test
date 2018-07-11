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
    public class ItTechnicianActivity : AbstractActivity<IItFormDataProcessing>, IActivity<IItFormDataProcessing>
    {

        private IItFormDataProcessing _itFormDataProcessing = null;
        public const string IT_TECHNINICAN_DONE_ACTION = "Done";
     

        public ItTechnicianActivity() : base(ItRequestFormBC.IT_TECHNECIAN)
        {
            InitFormDataProcessing();

            AddAction(new DefaultAction<IItFormDataProcessing>(IT_TECHNINICAN_DONE_ACTION, _itFormDataProcessing));
        }

        private void InitFormDataProcessing()
        {
            _itFormDataProcessing = new ItFromDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = false,
                IsSaveAttachments = true,
            };
        }

    }
}
