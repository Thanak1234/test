using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.EOMRequestForm;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.AvbRequestForm;
using Workflow.Web.Models.EOM;

namespace Workflow.Web.Service.Controllers
{
    public class EOMRequestController : AbstractServiceController<EOMRequestWorkflowInstance, EOMRequestFormViewModel>
    {

        public EOMRequestController() : base(){ }

        protected override EOMRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new EOMRequestFormViewModel();
        }

        protected override void MoreMapDataBC(EOMRequestFormViewModel viewData, EOMRequestWorkflowInstance workflowInstance)
        {
            workflowInstance.EOMInfo = viewData.dataItem.EOMInfo.TypeAs<EOMViewModel>();
        }

        protected override void MoreMapDataView(EOMRequestWorkflowInstance workflowInstance, EOMRequestFormViewModel viewData)
        {
            viewData.dataItem.EOMInfo = workflowInstance.EOMInfo.TypeAs<EOMViewModel>();
        }

        protected override IRequestFormService<EOMRequestWorkflowInstance> GetRequestformService()
        {
            return new EOMRequestFormService();
        }
    }
}