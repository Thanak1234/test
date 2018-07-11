using System.Collections.Generic;
using Workflow.Domain.Entities.BCJ;

namespace Workflow.Service.Interfaces {
    public partial interface IBcjRequestContentService {
        IEnumerable<CapexCategory> GetCapexCategories();
    }
}
