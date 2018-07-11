
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.EGM;

namespace Workflow.DataAcess.Repositories.EGMAttandance
{
    public class ATDDetailTypeRepository : RepositoryBase<AttandanceDetailType>, IRepository<AttandanceDetailType>
    {
        public ATDDetailTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
