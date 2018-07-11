using Workflow.Domain.Entities.Core;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.AvirForm;

namespace Workflow.Web.Service.Controllers {
    public class AvirFormController : AbstractServiceController<AvirFormWorkflowInstance, AvirFormViewModel> {

        protected override AvirFormViewModel CreateNewFormDataViewModel() {
            return new AvirFormViewModel();
        }

        protected override IRequestFormService<AvirFormWorkflowInstance> GetRequestformService() {
            return new AvirFormService();
        }

        protected override void MoreMapDataBC(AvirFormViewModel viewData, AvirFormWorkflowInstance workflowInstance) {
            if (viewData.dataItem.FormRequestData != null) {
                workflowInstance.FormRequestData = viewData.dataItem.FormRequestData.TypeAs<Avir>();
            }
        }

        protected override void MoreMapDataView(AvirFormWorkflowInstance workflowInstance, AvirFormViewModel viewData) {
            if (workflowInstance.FormRequestData != null) {
                viewData.dataItem.FormRequestData = workflowInstance.FormRequestData.TypeAs<Avir>();
            }
        }
    }
}
