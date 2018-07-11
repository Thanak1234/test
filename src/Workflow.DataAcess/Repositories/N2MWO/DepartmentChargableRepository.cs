using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.N2MWO;

namespace Workflow.DataAcess.Repositories.N2MWO {

    public class N2DepartmentChargableRepository : RepositoryBase<N2DepartmentChargable>, IN2DepartmentChargableRepository
    {
        public N2DepartmentChargableRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

}
