using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.IRF;

namespace Workflow.DataAcess.Repositories.IRF {
    public class IRFRequestItemRepository : RepositoryBase<IRFRequestItem>, IIRFRequestItemRepository {
        public IRFRequestItemRepository(IDbFactory dbFactory) : base(dbFactory) {
        }
    }
}
