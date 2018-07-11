using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject;
using Workflow.Domain.Entities.MWO;

namespace Workflow.DataAcess.Repositories.MWO {

    public class InformationRepository : RepositoryBase<MWOInformation>, IInformationRepository
    {
        public InformationRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

}
