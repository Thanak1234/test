/**
*@author : Phanny
*/
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.Core;

namespace Workflow.DataAcess.Repositories
{
    public class AvirRepository : RepositoryBase<Avir>, IAvirRepository {    
        public AvirRepository(IDbFactory dbFactory) : base(dbFactory) {
            
        }
    }
}
