using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories
{
    public interface IBestPerformanceRepository : IRepository<BestPerformance>
    {
        BestPerformance GetByRequestHeader(int id);
    }
}
