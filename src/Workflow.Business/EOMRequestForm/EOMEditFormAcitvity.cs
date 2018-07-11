

using Workflow.MSExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.EOMRequestForm {
    public class EOMEditFormAcitvity : AbstractActivity<IEOMFormDataProcessing>, IActivity<IEOMFormDataProcessing>
    {

        private IEOMFormDataProcessing _EOMFormDataProcessing = null;

        private Func<IEmailData> _GetEmailData;

        public EOMEditFormAcitvity(Func<IEmailData> GetEmailData) : base(EOMRequestFormBC.EDIT)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<IEOMFormDataProcessing>(AbstractAction<IEOMFormDataProcessing>.EDITED_ACTION, _EOMFormDataProcessing));
            _GetEmailData = GetEmailData;
        }

        public override IEmailData Email { get { return _GetEmailData() ; } }

        private void InitFormDataProcessing()
        {
            _EOMFormDataProcessing = new EOMFormDataProcessing() {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = true,
                IsUpdateLastActivity = true,
                IsSaveRequestData = true,
                IsSaveAttachments = true,
                TriggerWorkflow = false
            };
        }

    }
}
