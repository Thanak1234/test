using System.Linq;
using System.Web.Http;
using Workflow.Core.Attributes;
using Workflow.Domain.Entities.ITEIRQ;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Models.ITEIRQ;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    [RoutePrefix("api/iteirqrequest")]
    public class ITEIRQRequestController : AbstractServiceController<ITEIRQRequestWorkflowInstance, ITEIRQRequestFormViewModel>
    {
        #region Business Form Logic
        protected override void MoreMapDataBC(ITEIRQRequestFormViewModel viewData, ITEIRQRequestWorkflowInstance workflowInstance)
        {
            // EventInternet Form Base
            workflowInstance.EventInternet = viewData.dataItem.eventInternet.TypeAs<EventInternet>();
            
            // Asset Control Details - Collection
            workflowInstance.AddQuotations = (from p in viewData.dataItem.addQuotations select p.TypeAs<Quotation>());
            workflowInstance.DelQuotations = (from p in viewData.dataItem.delQuotations select p.TypeAs<Quotation>());
            workflowInstance.EditQuotations = (from p in viewData.dataItem.editQuotations select p.TypeAs<Quotation>());
        }

        protected override void MoreMapDataView(ITEIRQRequestWorkflowInstance workflowInstance, ITEIRQRequestFormViewModel viewData)
        {
            // Bind EventInternet data to view model
            viewData.dataItem.eventInternet = workflowInstance.EventInternet.TypeAs<EventInternetViewModel>();

            // Cast and bind [Asset Control Details] data list to view model
            if (workflowInstance.Quotations != null && workflowInstance.Quotations.Count() > 0)
            {
                viewData.dataItem.quotations = (from p in workflowInstance.Quotations select p.TypeAs<QuotationViewModel>());
            }
        }
        
        #endregion

        #region Constructor
        public ITEIRQRequestController()
        {

        }
        // Initialize constructor of view model
        protected override ITEIRQRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new ITEIRQRequestFormViewModel();
        }

        protected override IRequestFormService<ITEIRQRequestWorkflowInstance> GetRequestformService()
        {
            return new ITEIRQRequestFormService();
        }
        #endregion

    }
}