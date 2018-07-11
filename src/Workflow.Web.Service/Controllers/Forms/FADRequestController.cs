using System.Web.Http;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.FAD;
using Workflow.Domain.Entities.Finance;
using Workflow.Service;
using Workflow.Core.Attributes;
using System.Linq;
using Workflow.DataAcess.Repositories;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    [RoutePrefix("api/fadrequest")]
    public class FADRequestController : AbstractServiceController<FADRequestWorkflowInstance, FADRequestFormViewModel>
    {
        #region Business Form Logic
        protected override void MoreMapDataBC(FADRequestFormViewModel viewData, FADRequestWorkflowInstance workflowInstance)
        {
            // AssetDisposal Form Base
            workflowInstance.AssetDisposal = viewData.dataItem.assetDisposal.TypeAs<AssetDisposal>();

            // Asset Disposal Details - Collection
            workflowInstance.AddAssetDisposalDetails = (from p in viewData.dataItem.addAssetDisposalDetails select p.TypeAs<AssetDisposalDetail>());
            workflowInstance.DelAssetDisposalDetails = (from p in viewData.dataItem.delAssetDisposalDetails select p.TypeAs<AssetDisposalDetail>());
            workflowInstance.EditAssetDisposalDetails = (from p in viewData.dataItem.editAssetDisposalDetails select p.TypeAs<AssetDisposalDetail>());

            // Asset Control Details - Collection
            workflowInstance.AddAssetControlDetails = (from p in viewData.dataItem.addAssetControlDetails select p.TypeAs<AssetControlDetail>());
            workflowInstance.DelAssetControlDetails = (from p in viewData.dataItem.delAssetControlDetails select p.TypeAs<AssetControlDetail>());
            workflowInstance.EditAssetControlDetails = (from p in viewData.dataItem.editAssetControlDetails select p.TypeAs<AssetControlDetail>());
        }

        protected override void MoreMapDataView(FADRequestWorkflowInstance workflowInstance, FADRequestFormViewModel viewData)
        {
            // Bind AssetDisposal data to view model
            viewData.dataItem.assetDisposal = workflowInstance.AssetDisposal.TypeAs<AssetDisposalViewModel>();

            // Cast and bind [Asset Disposal Details] data list to view model
            if (workflowInstance.AssetDisposalDetails != null && workflowInstance.AssetDisposalDetails.Count() > 0)
            {
                viewData.dataItem.assetDisposalDetails = (from p in workflowInstance.AssetDisposalDetails select p.TypeAs<AssetDisposalDetailViewModel>());
            }

            // Cast and bind [Asset Control Details] data list to view model
            if (workflowInstance.AssetControlDetails != null && workflowInstance.AssetControlDetails.Count() > 0)
            {
                viewData.dataItem.assetControlDetails = (from p in workflowInstance.AssetControlDetails select p.TypeAs<AssetControlDetailViewModel>());
            }
        }

        [HttpGet]
        [Route("asset-category")]
        public HttpResponseMessage GetAssetCategory() {
            string sqlQuery = @"SELECT id, [NAME] [value] FROM [FINANCE].[CAPEX_CATEGORY] WHERE [ACTIVE] = 1 ORDER BY [NAME] ";
            var repository = new Repository();
            return Request.CreateResponse(HttpStatusCode.OK, new Repository().ExecDynamicSqlQuery(sqlQuery));
        }
        #endregion

        #region Constructor
        public FADRequestController()
        {

        }
        // Initialize constructor of view model
        protected override FADRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new FADRequestFormViewModel();
        }

        protected override IRequestFormService<FADRequestWorkflowInstance> GetRequestformService()
        {
            return new FADRequestFormService();
        }
        #endregion

    }
}