using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories
{
    public interface IBestPerformanceDetailRepository : IRepository<BestPerformanceDetail>
    {
        IList<BestPerformanceDetail> GetByRequestHeaderId(int id);
        IEnumerable<BestPerformanceDetail> GetByRequestHeaderId(int id, string type);
    }
}
