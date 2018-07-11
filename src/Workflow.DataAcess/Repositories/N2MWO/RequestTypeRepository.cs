using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.N2MWO;

namespace Workflow.DataAcess.Repositories.N2MWO
{

    public class N2RequestTypeRepository : RepositoryBase<N2RequestType>, IN2RequestTypeRepository
    {
        public N2RequestTypeRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

}
