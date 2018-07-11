using System.Linq;
using System.Web.Http;
using Workflow.Core.Attributes;
using Workflow.Domain.Entities.Forms;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Models.JRAM;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    [RoutePrefix("api/jramrequest")]
    public class JRAMRequestController : AbstractServiceController<JRAMRequestWorkflowInstance, JRAMRequestFormViewModel>
    {
        #region Business Form Logic
        protected override void MoreMapDataBC(JRAMRequestFormViewModel viewData, JRAMRequestWorkflowInstance workflowInstance)
        {
            // RamClear Form Base
            workflowInstance.RamClear = viewData.dataItem.ramClear.TypeAs<RamClear>();
        }

        protected override void MoreMapDataView(JRAMRequestWorkflowInstance workflowInstance, JRAMRequestFormViewModel viewData)
        {
            // Bind RamClear data to view model
            viewData.dataItem.ramClear = workflowInstance.RamClear.TypeAs<RamClearViewModel>();
        }
        
        #endregion

        #region Constructor
        public JRAMRequestController()
        {

        }
        // Initialize constructor of view model
        protected override JRAMRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new JRAMRequestFormViewModel();
        }

        protected override IRequestFormService<JRAMRequestWorkflowInstance> GetRequestformService()
        {
            return new JRAMRequestFormService();
        }
        #endregion

    }
}