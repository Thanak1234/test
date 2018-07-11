using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.MTF;

namespace Workflow.DataAcess.Repositories.MTF
{
    public interface ITreatmentRepository : IRepository<Treatment>
    {
        Treatment GetByRequestHeader(int id);
        string GetPendingTreamentMessage();
        void UpdateDayLeave(int requestHeaderId);
        void DeleteUniftTW(int requestHeaderId);
        string GetSubjectEmail(int requestHeaderId);
    }
}
