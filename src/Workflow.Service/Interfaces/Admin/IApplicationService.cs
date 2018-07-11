using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Core;

namespace Workflow.Service.Interfaces.Admin {

    public interface IApplicationService {
        RequestApplication AddApplication(RequestApplication entity);
        void DeleteApplication(RequestApplication entity);
        IEnumerable<RequestApplication> GetApplications();
        RequestApplication UpdateApplication(RequestApplication entity);
        RequestApplication GetSingle(int id);
    }
}
