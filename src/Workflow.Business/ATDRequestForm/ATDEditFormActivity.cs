using Workflow.MSExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.ATDRequestForm
{
    public class ATDEditFormActivity : AbstractActivity<IATDFormDataProcessing>,IActivity<IATDFormDataProcessing>
    {
        IATDFormDataProcessing _FormDataProcessing = null;
        private Func<IEmailData> _GetEmailData = null;

        public ATDEditFormActivity(Func<IEmailData> GetEmailData) : base(ATDRequestFormBC.EDIT)
        {
            this.InitFormDataProcessing();

            this.AddAction(new DefaultAction<IATDFormDataProcessing>(AbstractAction<IATDFormDataProcessing>.EDITED_ACTION, _FormDataProcessing));
            //this.AddAction(new DefaultAction<IATDFormDataProcessing>(AbstractAction<IATDFormDataProcessing>.CANCELED_ACTION, _CancelledFormDataProcessing));

            _GetEmailData = GetEmailData;
        }

        public override IEmailData Email { get { return _GetEmailData(); } }

        private void InitFormDataProcessing()
        {
            _FormDataProcessing = new ATDFormDataProcessing()
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

            //_CancelledFormDataProcessing = new ATDFormDataProcessing()
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
