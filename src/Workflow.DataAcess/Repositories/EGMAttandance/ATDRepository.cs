using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.EGM;

namespace Workflow.DataAcess.Repositories.EGMAttandance
{
    public class ATDRepository : RepositoryBase<Attandance>,IATDRepository
    {
        public ATDRepository(IDbFactory dbWorkflow) : base(dbWorkflow)
        {

        }
    }
}
