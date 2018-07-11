using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BCJ;

namespace Workflow.DataAcess.Repositories.BCJ
{
    public interface IRequestItemRepository : IRepository<BcjRequestItem>
    {
        IEnumerable<BcjRequestItem> GetByProjectDetail(int projectDetailId);
    }
}
