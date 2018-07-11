using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.MWO;

namespace Workflow.DataAcess.Repositories.MWO {

    public class RequestTypeRepository: RepositoryBase<RequestType>, IRequestTypeRepository {
        public RequestTypeRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

}
