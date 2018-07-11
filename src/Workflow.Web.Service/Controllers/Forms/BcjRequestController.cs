using System;
using System.Collections.Generic;
using System.Web.Http;
using Workflow.Domain.Entities.BCJ;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.BcjRequestForm;

namespace Workflow.Web.Service.Controllers
{
    public class BcjRequestController : AbstractServiceController<BcjRequestWorkflowInstance, BcjRequestFormViewModel>
    {
        protected override BcjRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new BcjRequestFormViewModel();
        }

        protected override IRequestFormService<BcjRequestWorkflowInstance> GetRequestformService()
        {
            return new BcjRequestFormService();
        }

        protected override void MoreMapDataBC(BcjRequestFormViewModel viewData, BcjRequestWorkflowInstance workflowInstance)
        {
            workflowInstance.ProjectDetail = viewData.dataItem.projectDetail.TypeAs<ProjectDetail>();

            workflowInstance.AddRequestItems = CreateRequestItem(viewData.dataItem.addRequestItems);
            workflowInstance.DelRequestItems = CreateRequestItem(viewData.dataItem.delRequestItems);
            workflowInstance.EditRequestItems = CreateRequestItem(viewData.dataItem.editRequestItems);

            workflowInstance.AddAnalysisItems = CreateAnalysisItem(viewData.dataItem.addAnalysisItems);
            workflowInstance.DelAnalysisItems = CreateAnalysisItem(viewData.dataItem.delAnalysisItems);
            workflowInstance.EditAnalysisItems = CreateAnalysisItem(viewData.dataItem.editAnalysisItems);

            workflowInstance.AddPurchaseOrderItems = CreatePurchaseOrderItem(viewData.dataItem.addPurchaseOrderItems);
            workflowInstance.DelPurchaseOrderItems = CreatePurchaseOrderItem(viewData.dataItem.delPurchaseOrderItems);
            workflowInstance.EditPurchaseOrderItems = CreatePurchaseOrderItem(viewData.dataItem.editPurchaseOrderItems);
        }

        protected override void MoreMapDataView(BcjRequestWorkflowInstance workflowInstance, BcjRequestFormViewModel viewData)
        {
            viewData.dataItem.projectDetail = workflowInstance.ProjectDetail.TypeAs<ProjectDetailViewModel>();
           
            var requestItems = new List<RequestItemViewModel>();
            if (workflowInstance.RequestItems != null) {
                foreach (var item in workflowInstance.RequestItems)
                {
                    item.ProjectDetail = null;
                    var model = item.TypeAs<RequestItemViewModel>();
                    requestItems.Add(model);
                }
            }

            var analysisItems = new List<AnalysisItemViewModel>();
            if (workflowInstance.AnalysisItems != null) {
                foreach (var item in workflowInstance.AnalysisItems)
                {
                    item.ProjectDetail = null;
                    var model = item.TypeAs<AnalysisItemViewModel>();
                    analysisItems.Add(model);
                }
            }

            var purchaseOrderItems = new List<PurchaseOrderItemViewModel>();
            if (workflowInstance.PurchaseOrderItems != null)
            {
                foreach (var item in workflowInstance.PurchaseOrderItems)
                {
                    var model = item.TypeAs<PurchaseOrderItemViewModel>();
                    purchaseOrderItems.Add(model);
                }
            }

            viewData.dataItem.requestItems = requestItems;
            viewData.dataItem.analysisItems = analysisItems;
            viewData.dataItem.purchaseOrderItems = purchaseOrderItems;
        }
        
        #region Private Method

        private IEnumerable<BcjRequestItem> CreateRequestItem(IEnumerable<RequestItemViewModel> items)
        {
            var requestItems = new List<BcjRequestItem>();

            foreach (var item in items)
            {
                requestItems.Add(item.TypeAs<BcjRequestItem>());
            }

            return requestItems;
        }

        private IEnumerable<AnalysisItem> CreateAnalysisItem(IEnumerable<AnalysisItemViewModel> items)
        {
            var requestItems = new List<AnalysisItem>();

            foreach (var item in items)
            {
                requestItems.Add(item.TypeAs<AnalysisItem>());
            }

            return requestItems;
        }

        private IEnumerable<PurchaseOrder> CreatePurchaseOrderItem(IEnumerable<PurchaseOrderItemViewModel> items)
        {
            if (items == null) return new List<PurchaseOrder>();
            var requestItems = new List<PurchaseOrder>();

            foreach (var item in items)
            {
                requestItems.Add(item.TypeAs<PurchaseOrder>());
            }

            return requestItems;
        }

        #endregion

        [HttpGet]
        [Route("api/bcjrequest/capexcategories")]
        public IEnumerable<CapexCategory> GetCapexCategories() {
            IBcjRequestContentService contentService = new BcjRequestContentService();
            return contentService.GetCapexCategories();
        }
    }
}