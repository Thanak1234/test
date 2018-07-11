using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Admsr;
using Workflow.Domain.Entities.ITCR;

namespace Workflow.DataAcess.Repositories.ITCR
{
    public class RequestFormDataRepository : RepositoryBase<RequestFormData>, IRequestFormDataRepository
    {
        public RequestFormDataRepository(IDbFactory dbFactory) : base(dbFactory) {
        }
    }
}
