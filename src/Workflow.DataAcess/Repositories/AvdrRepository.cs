/**
*@author : Phanny
*/
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.Core;

namespace Workflow.DataAcess.Repositories
{
    public class AvdrRepository : RepositoryBase<Avdr>, IAvdrRepository {
    
        public AvdrRepository(IDbFactory dbFactory) : base(dbFactory) {
            
        }
    }
}
