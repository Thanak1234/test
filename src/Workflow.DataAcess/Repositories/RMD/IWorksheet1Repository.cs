using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.RMD;

namespace Workflow.DataAcess.Repositories.RMD
{
    public interface IWorksheet1Repository : IRepository<Worksheet1>
    {
        IList<Worksheet1> GetByRequestHeaderId(int id);
    }
}
