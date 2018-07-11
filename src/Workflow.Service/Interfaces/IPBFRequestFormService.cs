using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.AV;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.PBF;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Service.Interfaces
{
    public interface IPBFRequestFormService : IRequestFormService<PBFRequestWorkflowInstance>
    {
        IEnumerable<Specification> GetSpecItems(int projectDetailId);
    }
}
