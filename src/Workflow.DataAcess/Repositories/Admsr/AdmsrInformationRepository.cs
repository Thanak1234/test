using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Admsr;

namespace Workflow.DataAcess.Repositories.Admsr
{
    public class AdmsrInformationRepository : RepositoryBase<AdmsrInformation>, IAdmsrInformationRepository {

        public AdmsrInformationRepository(IDbFactory dbFactory) : base(dbFactory) {
        }
    }
}
