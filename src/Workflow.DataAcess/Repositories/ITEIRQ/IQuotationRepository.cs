using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.ITEIRQ;

namespace Workflow.DataAcess.Repositories.ITEIRQ
{
    public interface IQuotationRepository : IRepository<Quotation>
    {
        IList<Quotation> GetByRequestHeaderId(int id);
    }
}
