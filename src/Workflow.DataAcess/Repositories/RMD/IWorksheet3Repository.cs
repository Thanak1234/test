using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.RMD;

namespace Workflow.DataAcess.Repositories.RMD
{
    public interface IWorksheet3Repository : IRepository<Worksheet3>
    {
        IList<Worksheet3> GetByRequestHeaderId(int id);
    }
}
