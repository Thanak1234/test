using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Finance;

namespace Workflow.DataAcess.Repositories.FAD
{
    public interface IAssetDisposalRepository : IRepository<AssetDisposal>
    {
        AssetDisposal GetByRequestHeader(int id);
    }
}
