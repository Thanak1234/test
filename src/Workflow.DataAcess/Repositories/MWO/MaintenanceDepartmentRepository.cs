using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.MWO;

namespace Workflow.DataAcess.Repositories {
    public class MaintenanceDepartmentRepository : RepositoryBase<MaintenanceDepartment>, IMaintenanceDepartmentRepository {

        public MaintenanceDepartmentRepository(IDbFactory dbFactory) : base(dbFactory) {

        }
    }
}
