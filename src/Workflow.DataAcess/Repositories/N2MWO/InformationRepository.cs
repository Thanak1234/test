using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject;
using Workflow.Domain.Entities.N2MWO;

namespace Workflow.DataAcess.Repositories.N2MWO
{

    public class N2InformationRepository : RepositoryBase<N2MWOInformation>, IN2InformationRepository
    {
        public N2InformationRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

}
