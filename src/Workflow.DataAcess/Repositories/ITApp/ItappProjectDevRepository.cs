using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Core.ITApp;

namespace Workflow.DataAcess.Repositories.ITApp {
    public class ItappProjectDevRepository : RepositoryBase<ItappProjectDev>, IItappProjectDevRepository {
        public ItappProjectDevRepository(IDbFactory dbFactory) : base(dbFactory) {
        }
    }
}
