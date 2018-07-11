using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.HR;

namespace Workflow.DataAcess.Repositories.HumanResource
{
    public class DestinationRepository:RepositoryBase<Destination>, IDestinationRepository
    {
        public DestinationRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
