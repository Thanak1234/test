using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Admsr;
using Workflow.Domain.Entities.ITAD;

namespace Workflow.DataAcess.Repositories.ITAD
{
    public class ITADEmployeeRepository : RepositoryBase<ITADEmployee>, IITADEmployeeRepository
    {
        public ITADEmployeeRepository(IDbFactory dbFactory) : base(dbFactory) {
        }
    }
}
