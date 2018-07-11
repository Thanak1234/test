using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories.JRAM
{
    public interface IRamClearRepository : IRepository<RamClear>
    {
        RamClear GetByRequestHeader(int id);
    }
}
