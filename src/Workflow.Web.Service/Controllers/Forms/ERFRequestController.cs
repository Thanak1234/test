using Workflow.Domain.Entities.HumanResource;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.HumanResource.CRF;

namespace Workflow.Web.Service.Controllers
{
    public class ERFRequestController : AbstractServiceController<ERFRequestWorkflowInstance, RequestFormViewModel>
    {
        private IERFRequestFormService service;
        
        protected override RequestFormViewModel CreateNewFormDataViewModel()
        {
            return new RequestFormViewModel();
        }
        
        protected override IRequestFormService<ERFRequestWorkflowInstance> GetRequestformService()
        {
            service = new ERFRequestFormService();
            return service;
        }
        
        protected override void MoreMapDataBC(RequestFormViewModel viewData, ERFRequestWorkflowInstance workflowInstance)
        {
            workflowInstance.Requisition = viewData.dataItem.requisition.TypeAs<Requisition>();
        }
        
        protected override void MoreMapDataView(ERFRequestWorkflowInstance workflowInstance, RequestFormViewModel viewData)
        {
            viewData.dataItem.requisition = workflowInstance.Requisition.TypeAs<RequisitionViewModel>();
        }
    }
}