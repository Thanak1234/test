using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.IRF;

namespace Workflow.DataAcess.Repositories.IRF {
    public class IRFVendorRepository : RepositoryBase<IRFVendor>, IIRFVendorRepository {
        public IRFVendorRepository(IDbFactory dbFactory) : base(dbFactory) {
        }
    }
}
