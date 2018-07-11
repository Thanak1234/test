using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.EGM;

namespace Workflow.DataAcess.Repositories.EGMMachine
{
    public class MCNRepository : RepositoryBase<Machine>,IMCNRepository
    {
        public MCNRepository(IDbFactory dbFactory) :base(dbFactory)
        { 
            
        }
    }
}
