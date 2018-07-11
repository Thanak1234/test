/**
*@author : Phanny
*/
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.Queue;

namespace Workflow.DataAcess.Repositories
{
    public class FingerprintRepository : RepositoryBase<FingerPrintMachine>, IFingerprintRepository {
    
        public FingerprintRepository(IDbFactory dbFactory) : base(dbFactory) {
            
        }
    }
}
