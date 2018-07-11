using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.Core.CCR;

namespace Workflow.DataAcess.Repositories {
    public class ContractDraftRepository : RepositoryBase<ContractDraft>, IContractDraftRepository {
    
        public ContractDraftRepository(IDbFactory dbFactory) : base(dbFactory) {
            
        }
    }
}
