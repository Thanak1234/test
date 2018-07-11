using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Finance;

namespace Workflow.DataAcess.Repositories.FAT
{
    public interface IAssetTransferDetailRepository : IRepository<AssetTransferDetail>
    {
        IList<AssetTransferDetail> GetByRequestHeaderId(int id);
    }
}
