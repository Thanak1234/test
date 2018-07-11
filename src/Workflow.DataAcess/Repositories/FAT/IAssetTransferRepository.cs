using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Finance;

namespace Workflow.DataAcess.Repositories.FAT
{
    public interface IAssetTransferRepository : IRepository<AssetTransfer>
    {
        AssetTransfer GetByRequestHeader(int id);
    }
}
