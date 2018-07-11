using Workflow.MSExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.MCNRequestForm
{
    public class MCNEditFormActivity : AbstractActivity<IMCNFormDataProcessing>,IActivity<IMCNFormDataProcessing>
    {
        IMCNFormDataProcessing _formDataProcessing = null;
        IMCNFormDataProcessing _cancelledFormDataProcessing = null;

        private Func<IEmailData> _GetEmailData = null;

        public MCNEditFormActivity(Func<IEmailData> GetEmailData) : base(MCNRequestFormBC.EDIT)
        {
            this.InitFormDataProcessing();
            this.AddAction(new DefaultAction<IMCNFormDataProcessing>(AbstractAction<IMCNFormDataProcessing>.EDITED_ACTION, _formDataProcessing));
            //this.AddAction(new DefaultAction<IMCNFormDataProcessing>(AbstractAction<IMCNFormDataProcessing>.CANCELED_ACTION, _cancelledFormDataProcessing));

            _GetEmailData = GetEmailData;
        }

        public override IEmailData Email
        {
            get
            {
                return this._GetEmailData();
            }

            
        }

        private void InitFormDataProcessing()
        {
            _formDataProcessing = new MCNFormDataProcessing()
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

            //_cancelledFormDataProcessing = new MCNFormDataProcessing()
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
