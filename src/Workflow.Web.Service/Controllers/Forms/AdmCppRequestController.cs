using System;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.AdmCpp;

namespace Workflow.Web.Service.Controllers {

    public class AdmCppRequestController : AbstractServiceController<AdmCppRequestWorkflowInstance, AdmCppRequestFormViewModel>
    {

        public AdmCppRequestController() : base(){ }

        protected override AdmCppRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new AdmCppRequestFormViewModel();
        }

        protected override IRequestFormService<AdmCppRequestWorkflowInstance> GetRequestformService()
        {
            return new AdmCppRequestFormService();
        }

        protected override void MoreMapDataBC(AdmCppRequestFormViewModel viewData, AdmCppRequestWorkflowInstance workflowInstance) {
            workflowInstance.AdmCpp = viewData.dataItem.AdmCpp.TypeAs<AdmCppViewModel>();
        }

        protected override void MoreMapDataView(AdmCppRequestWorkflowInstance workflowInstance, AdmCppRequestFormViewModel viewData) {
            viewData.dataItem.AdmCpp = workflowInstance.AdmCpp.TypeAs<AdmCppViewModel>();
        }
    }
}