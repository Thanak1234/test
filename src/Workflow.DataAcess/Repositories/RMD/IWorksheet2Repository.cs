using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.RMD;

namespace Workflow.DataAcess.Repositories.RMD
{
    public interface IWorksheet2Repository : IRepository<Worksheet2>
    {
        IList<Worksheet2> GetByRequestHeaderId(int id);
    }
}
