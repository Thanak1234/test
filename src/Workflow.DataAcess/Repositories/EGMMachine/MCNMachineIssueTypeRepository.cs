
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.EGM;

namespace Workflow.DataAcess.Repositories.EGMMachine
{
    public class MCNMachineIssueTypeRepository : RepositoryBase<MachineIssueType>, IRepository<MachineIssueType>
    {
        public MCNMachineIssueTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
