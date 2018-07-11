
using System.Collections.Generic;
/**
*@author : Yim Samaune
*/
using Workflow.Domain.Entities.Finance;

namespace Workflow.Domain.Entities.BatchData
{
    public class FADRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public AssetDisposal AssetDisposal { get; set; }

        public IEnumerable<AssetDisposalDetail> AssetDisposalDetails { get; set; }
        public IEnumerable<AssetDisposalDetail> DelAssetDisposalDetails { get; set; }
        public IEnumerable<AssetDisposalDetail> EditAssetDisposalDetails { get; set; }
        public IEnumerable<AssetDisposalDetail> AddAssetDisposalDetails { get; set; }

        public IEnumerable<AssetControlDetail> AssetControlDetails { get; set; }
        public IEnumerable<AssetControlDetail> DelAssetControlDetails { get; set; }
        public IEnumerable<AssetControlDetail> EditAssetControlDetails { get; set; }
        public IEnumerable<AssetControlDetail> AddAssetControlDetails { get; set; }
        
    }
}