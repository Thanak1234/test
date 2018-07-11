using Workflow.MSExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.ICDRequestForm
{
    public class ICDEditFormActivity : AbstractActivity<IICDFormDataProcessing>,IActivity<IICDFormDataProcessing>
    {
        private IICDFormDataProcessing _ICDFormDataProcessing = null;
        private IICDFormDataProcessing _ICDFormCancelDataProcessing = null;

        private Func<IEmailData> _GetEmailData = null;

        public ICDEditFormActivity(Func<IEmailData> GetEmailData) : base(ICDRequestFormBC.EDIT)
        {
            this.InitFormDataProcessing();

            this.AddAction(new DefaultAction<IICDFormDataProcessing>(AbstractAction<IICDFormDataProcessing>.EDITED_ACTION, _ICDFormDataProcessing));
            //this.AddAction(new DefaultAction<IICDFormDataProcessing>(AbstractAction<IICDFormDataProcessing>.CANCELED_ACTION, _ICDFormCancelDataProcessing));

            _GetEmailData = GetEmailData;
        }

        public override IEmailData Email { get { return _GetEmailData(); } }

        private void InitFormDataProcessing()
        {
            _ICDFormDataProcessing = new ICDFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = true,
                IsUpdateLastActivity = true,
                IsSaveRequestData = true,
                IsSaveAttachments = true,
                TriggerWorkflow = false
            };

            //_ICDFormCancelDataProcessing = new ICDFormDataProcessing()
            //{
            //    IsAddNewRequestHeader = false,
            //    IsEditPriority = false,
            //    IsEditRequestor = false,
            //    IsSaveActivityHistory = true,
            //    IsUpdateLastActivity = true,
            //    IsSaveRequestData = true,
            //    IsSaveAttachments = true,
            //    TriggerWorkflow = false
            //};
        }

    }
}
