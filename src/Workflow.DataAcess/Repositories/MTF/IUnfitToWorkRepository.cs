using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.MTF;

namespace Workflow.DataAcess.Repositories.MTF
{
    public interface IUnfitToWorkRepository : IRepository<UnfitToWork>
    {
        IList<UnfitToWork> GetByRequestHeader(int id);
    }
}
