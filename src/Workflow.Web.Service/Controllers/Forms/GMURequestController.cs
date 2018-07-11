using System.Linq;
using System.Web.Http;
using Workflow.Core.Attributes;
using Workflow.Domain.Entities.Forms;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Models.GMU;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    [RoutePrefix("api/gmurequest")]
    public class GMURequestController : AbstractServiceController<GMURequestWorkflowInstance, GmuRequestFormViewModel>
    {
        #region Business Form Logic
        protected override void MoreMapDataBC(GmuRequestFormViewModel viewData, GMURequestWorkflowInstance workflowInstance)
        {
            // RamClear Form Base
            workflowInstance.GmuRamClear = viewData.dataItem.gmuRamClear.TypeAs<GmuRamClear>();
        }

        protected override void MoreMapDataView(GMURequestWorkflowInstance workflowInstance, GmuRequestFormViewModel viewData)
        {
            // Bind RamClear data to view model
            viewData.dataItem.gmuRamClear = workflowInstance.GmuRamClear.TypeAs<GmuRamClearViewModel>();
        }
        
        #endregion

        #region Constructor
        public GMURequestController()
        {

        }
        // Initialize constructor of view model
        protected override GmuRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new GmuRequestFormViewModel();
        }

        protected override IRequestFormService<GMURequestWorkflowInstance> GetRequestformService()
        {
            return new GMURequestFormService();
        }
        #endregion

    }
}