using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories.GMU
{
    public interface IGmuRamClearRepository : IRepository<GmuRamClear>
    {
        GmuRamClear GetByRequestHeader(int id);
    }
}
