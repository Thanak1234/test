using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.Business.MwoRequestForm;
using Workflow.Domain.Entities.Core;
using Workflow.Domain.Entities.MWO;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.AvdrForm;

namespace Workflow.Web.Service.Controllers
{
    public class AvdrFormController : AbstractServiceController<AvdrFormWorkflowInstance, AvdrFormViewModel> {

        protected override AvdrFormViewModel CreateNewFormDataViewModel() {
            return new AvdrFormViewModel();
        }

        protected override IRequestFormService<AvdrFormWorkflowInstance> GetRequestformService() {
            return new AvdrFormService();
        }

        protected override void MoreMapDataBC(AvdrFormViewModel viewData, AvdrFormWorkflowInstance workflowInstance) {
            if(viewData.dataItem.FormRequestData != null) {
                workflowInstance.FormRequestData = viewData.dataItem.FormRequestData.TypeAs<Avdr>();
            }
        }

        protected override void MoreMapDataView(AvdrFormWorkflowInstance workflowInstance, AvdrFormViewModel viewData) {
            if(workflowInstance.FormRequestData != null) {
                viewData.dataItem.FormRequestData = workflowInstance.FormRequestData.TypeAs<Avdr>();
            }
        }
    }
}
