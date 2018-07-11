using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.MWO;

namespace Workflow.DataAcess.Repositories.MWO {

    public class WorkTypeRepository: RepositoryBase<WorkType>, IWorkTypeRepository {
        public WorkTypeRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

}
