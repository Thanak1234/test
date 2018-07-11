/**
*@author : Yim Samaune
*/
using System.Collections.Generic;
using Workflow.Domain.Entities.Finance;

namespace Workflow.Domain.Entities.BatchData
{
    public class FATRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public AssetTransfer AssetTransfer { get; set; }

        public IEnumerable<AssetTransferDetail> AssetTransferDetails { get; set; }
        public IEnumerable<AssetTransferDetail> DelAssetTransferDetails { get; set; }
        public IEnumerable<AssetTransferDetail> EditAssetTransferDetails { get; set; }
        public IEnumerable<AssetTransferDetail> AddAssetTransferDetails { get; set; }
    }
}