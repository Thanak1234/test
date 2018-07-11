using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.AV;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.BCJ;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Service.Interfaces
{
    public interface IBcjRequestFormService : IRequestFormService<BcjRequestWorkflowInstance>
    {
        IEnumerable<BcjRequestItem> GetRequestItems(int projectDetailId);
        IEnumerable<AnalysisItem> GetAnalysisItems(int projectDetailId);

        BcjRequestItem GetRequestItem(int id);
        AnalysisItem GetAnalysisItem(int id);
    }
}
