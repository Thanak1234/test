using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Finance;

namespace Workflow.DataAcess.Repositories.FAD
{
    public interface IAssetControlDetailRepository : IRepository<AssetControlDetail>
    {
        IList<AssetControlDetail> GetByRequestHeaderId(int id);
    }
}
