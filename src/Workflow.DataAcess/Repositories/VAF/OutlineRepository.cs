using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.VAF;

namespace Workflow.DataAcess.Repositories.VAF {
    public class OutlineRepository : RepositoryBase<Outline>, IOutlineRepository {

        public OutlineRepository(IDbFactory dbFactory) : base(dbFactory) {
        }
    }
}
