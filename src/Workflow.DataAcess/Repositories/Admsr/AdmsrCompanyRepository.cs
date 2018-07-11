using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Admsr;

namespace Workflow.DataAcess.Repositories.Admsr
{
    public class AdmsrCompanyRepository : RepositoryBase<AdmsrCompany>, IAdmsrCompanyRepository
    {
        public AdmsrCompanyRepository(IDbFactory dbFactory) : base(dbFactory) {
        }
    }
}
