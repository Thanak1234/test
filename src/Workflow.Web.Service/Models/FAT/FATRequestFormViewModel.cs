using System.Collections.Generic;

namespace Workflow.Web.Models.FAT
{
    public class FATRequestFormViewModel : AbstractFormDataViewModel
    {
        private DataItem _dataItem;

        public DataItem dataItem
        {
            get
            {
                return _dataItem ?? (_dataItem = new DataItem());
            }
            set { _dataItem = value; }
        }
    }

    public class DataItem
    {
        public AssetTransferViewModel assetTransfer { get; set; }
        public IEnumerable<AssetTransferDetailViewModel> assetTransferDetails { get; set; }
        public IEnumerable<AssetTransferDetailViewModel> addAssetTransferDetails { get; set; }
        public IEnumerable<AssetTransferDetailViewModel> editAssetTransferDetails { get; set; }
        public IEnumerable<AssetTransferDetailViewModel> delAssetTransferDetails { get; set; }
    }
}
