using System.Web.Http;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.FAT;
using Workflow.Domain.Entities.Finance;
using System.Linq;
using Workflow.Service;
using System.Net.Http;
using Workflow.Core.Attributes;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    [RoutePrefix("api/fatrequest")]
    public class FATRequestController : AbstractServiceController<FATRequestWorkflowInstance, FATRequestFormViewModel>
    {
        #region Constructor
        public FATRequestController()
        {

        }
        // Initialize constructor of view model
        protected override FATRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new FATRequestFormViewModel();
        }

        protected override IRequestFormService<FATRequestWorkflowInstance> GetRequestformService()
        {
            return new FATRequestFormService();
        }
        #endregion

        #region Business Form Logic
        protected override void MoreMapDataBC(FATRequestFormViewModel viewData, FATRequestWorkflowInstance workflowInstance)
        {
            // AssetTransfer Form Base
            workflowInstance.AssetTransfer = viewData.dataItem.assetTransfer.TypeAs<AssetTransfer>();

            // AssetTransferDetails - Collection
            workflowInstance.AddAssetTransferDetails = (from p in viewData.dataItem.addAssetTransferDetails select p.TypeAs<AssetTransferDetail>());
            workflowInstance.DelAssetTransferDetails = (from p in viewData.dataItem.delAssetTransferDetails select p.TypeAs<AssetTransferDetail>());
            workflowInstance.EditAssetTransferDetails = (from p in viewData.dataItem.editAssetTransferDetails select p.TypeAs<AssetTransferDetail>());
        }

        protected override void MoreMapDataView(FATRequestWorkflowInstance workflowInstance, FATRequestFormViewModel viewData)
        {
            // Bind AssetTransfer data to view model
            viewData.dataItem.assetTransfer = workflowInstance.AssetTransfer.TypeAs<AssetTransferViewModel>();

            // Cast and bind Unfit To Work data list to view model
            if (workflowInstance.AssetTransferDetails != null && workflowInstance.AssetTransferDetails.Count() > 0)
            {
                viewData.dataItem.assetTransferDetails = (from p in workflowInstance.AssetTransferDetails select p.TypeAs<AssetTransferDetailViewModel>());
            }

        }
        #endregion
    }
}