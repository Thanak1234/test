using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.MWO;

namespace Workflow.DataAcess.Repositories.MWO {

    public class DepartmentChargableRepository: RepositoryBase<DepartmentChargable>, IDepartmentChargableRepository {
        public DepartmentChargableRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

}
