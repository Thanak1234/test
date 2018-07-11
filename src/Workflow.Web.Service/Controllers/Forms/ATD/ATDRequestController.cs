using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.EGM;
using Workflow.Domain.Entities.BatchData.EGMInstance;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.AttandanceRequestForm;
using Workflow.Web.Models.ItRequestForm;

namespace Workflow.Web.Service.Controllers
{
    public class ATDRequestController : AbstractServiceController<ATDRequestWorkflowInstance, ATDFormViewModel>
    {
        public ATDRequestController()
        {

        }

        protected override ATDFormViewModel CreateNewFormDataViewModel()
        {
            return new ATDFormViewModel();
        }

        protected override IRequestFormService<ATDRequestWorkflowInstance> GetRequestformService()
        {
            return new ATDRequestFormService();
        }

        protected override void MoreMapDataBC(ATDFormViewModel viewData, ATDRequestWorkflowInstance workflowInstance)
        {
            workflowInstance.Attandance = viewData.dataItem.Attandance.TypeAs<Attandance>();

            workflowInstance.Attandance.created_by = workflowInstance.CurrentUser;
            workflowInstance.Attandance.created_date = DateTime.Now;

            workflowInstance.Attandance.modified_by = workflowInstance.CurrentUser;
            workflowInstance.Attandance.modified_date = DateTime.Now;

            workflowInstance.Attandance.request_header_id = viewData.requestHeaderId;

        }

        protected override void MoreMapDataView(ATDRequestWorkflowInstance workflowInstance, ATDFormViewModel viewData)
        {
            viewData.dataItem.Attandance = workflowInstance.Attandance.TypeAs<Attandance>();
        }
    }
}