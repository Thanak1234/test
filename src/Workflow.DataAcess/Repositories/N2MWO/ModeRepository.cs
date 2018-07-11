using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.N2MWO;

namespace Workflow.DataAcess.Repositories.N2MWO
{

    public class N2ModeRepository : RepositoryBase<N2Mode>, IN2ModeRepository
    {
        public N2ModeRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

}
