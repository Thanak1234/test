using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.OSHA;

namespace Workflow.DataAcess.Repositories.OSHA
{
    public class OSHAEmployeeRepository : RepositoryBase<OSHAEmployee>, IOSHAEmployeeRepository
    {
        public OSHAEmployeeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
