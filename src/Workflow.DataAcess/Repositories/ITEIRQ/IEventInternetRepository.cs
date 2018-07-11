using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.ITEIRQ;

namespace Workflow.DataAcess.Repositories.ITEIRQ
{
    public interface IEventInternetRepository : IRepository<EventInternet>
    {
        EventInternet GetByRequestHeader(int id);
    }
}
