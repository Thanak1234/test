

using Workflow.MSExchange;
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
    public class AvbEditFormAcitvity : AbstractActivity<IAvbFormDataProcessing>, IActivity<IAvbFormDataProcessing>
    {

        private IAvbFormDataProcessing _avbFormDataProcessing = null;

        private Func<IEmailData> _GetEmailData;

        public AvbEditFormAcitvity(Func<IEmailData> GetEmailData) : base(AvbRequestFormBC.EDIT)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IAvbFormDataProcessing>(AbstractAction<IAvbFormDataProcessing>.EDITED_ACTION, _avbFormDataProcessing));
            _GetEmailData = GetEmailData;
            //Email configuration
        }

        public override IEmailData Email { get { return _GetEmailData() ; } }

        private void InitFormDataProcessing()
        {
            _avbFormDataProcessing = new AvbFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = true,
                IsUpdateLastActivity = false,
                IsSaveRequestData = true,
                IsSaveAttachments = true,
                TriggerWorkflow = false
            };
        }

    }
}
