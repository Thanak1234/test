using System.Collections.Generic;

namespace Workflow.Web.Models.FAD
{
    public class FADRequestFormViewModel : AbstractFormDataViewModel
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
        public AssetDisposalViewModel assetDisposal { get; set; }

        public IEnumerable<AssetDisposalDetailViewModel> assetDisposalDetails { get; set; }
        public IEnumerable<AssetDisposalDetailViewModel> addAssetDisposalDetails { get; set; }
        public IEnumerable<AssetDisposalDetailViewModel> editAssetDisposalDetails { get; set; }
        public IEnumerable<AssetDisposalDetailViewModel> delAssetDisposalDetails { get; set; }

        public IEnumerable<AssetControlDetailViewModel> assetControlDetails { get; set; }
        public IEnumerable<AssetControlDetailViewModel> addAssetControlDetails { get; set; }
        public IEnumerable<AssetControlDetailViewModel> editAssetControlDetails { get; set; }
        public IEnumerable<AssetControlDetailViewModel> delAssetControlDetails { get; set; }
        
    }
}
