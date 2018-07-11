using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.RMD;

namespace Workflow.DataAcess.Repositories.RMD
{
    public interface IRiskAssessmentRepository : IRepository<RiskAssessment>
    {
        RiskAssessment GetByRequestHeader(int id);
    }
}
