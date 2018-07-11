using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BCJ;

namespace Workflow.DataAcess.Repositories.BCJ
{
    public interface IAnalysisItemRepository : IRepository<AnalysisItem>
    {
        IEnumerable<AnalysisItem> GetByProjectDetail(int projectDetailId);
    }
}
