using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.N2MWO;

namespace Workflow.DataAcess.Repositories {
    public class N2MaintenanceDepartmentRepository : RepositoryBase<N2MaintenanceDepartment>, IN2MaintenanceDepartmentRepository
    {

        public N2MaintenanceDepartmentRepository(IDbFactory dbFactory) : base(dbFactory) {

        }
    }
}
