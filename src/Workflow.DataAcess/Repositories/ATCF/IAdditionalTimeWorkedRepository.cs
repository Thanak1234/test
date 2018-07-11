using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Finance;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories.ATCF
{
    public interface IAdditionalTimeWorkedRepository : IRepository<AdditionalTimeWorked>
    {
        IList<AdditionalTimeWorked> GetByRequestHeaderId(int id);
    }
}
