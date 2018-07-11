/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.ITRequestForm;

namespace Workflow.Business.CRRRequestForm
{
    public class CRRReservationReviewActivity : AbstractActivity<ICRRFormDataProcessing>, IActivity<ICRRFormDataProcessing>
    {

        private ICRRFormDataProcessing _CRRFormDataProcessing = null;

        public CRRReservationReviewActivity() : base(CRRRequestFormBC.RESERVATION_REVIEW)
        {
            InitFormDataProcessing();
            AddAction(new DefaultAction<ICRRFormDataProcessing>(AbstractAction<ICRRFormDataProcessing>.COMPLETED_ACTION, _CRRFormDataProcessing));
            AddAction(new DefaultAction<ICRRFormDataProcessing>(AbstractAction<ICRRFormDataProcessing>.REWORKED_ACTION, _CRRFormDataProcessing));
            AddAction(new DefaultAction<ICRRFormDataProcessing>(AbstractAction<ICRRFormDataProcessing>.REJECTED_ACTION, _CRRFormDataProcessing));
        }
        private void InitFormDataProcessing()
        {
            _CRRFormDataProcessing = new CRRFormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = false,
                IsUpdateLastActivity = false,
                IsSaveRequestData = true,
                IsSaveAttachments = true
            };
        }
    }
}
