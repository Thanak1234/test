/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.EOMRequestForm;

namespace Workflow.Business.EOMRequestForm {
    public class EOMPayrolllActivity : AbstractActivity<IEOMFormDataProcessing>, IActivity<IEOMFormDataProcessing>
    {
        private IEOMFormDataProcessing _CRRFormDataProcessing = null;

        public EOMPayrolllActivity() : base(EOMRequestFormBC.PAYROLL)
        {
            InitFormDataProcessing();
            
            AddAction(new DefaultAction<IEOMFormDataProcessing>(AbstractAction<IEOMFormDataProcessing>.SUBMITTED_ACTION, _CRRFormDataProcessing));
            AddAction(new DefaultAction<IEOMFormDataProcessing>(AbstractAction<IEOMFormDataProcessing>.REWORKED_ACTION, _CRRFormDataProcessing));
        }

        private void InitFormDataProcessing()
        {
            _CRRFormDataProcessing = new EOMFormDataProcessing()
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
