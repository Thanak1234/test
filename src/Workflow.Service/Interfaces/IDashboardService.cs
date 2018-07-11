using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.DataObject.Dashboard;

namespace Workflow.Service.Interfaces {
    public interface IDashboardService {
        ResourceWrapper GetTasksByLoginName(TaskQueryParameter queryParameter);
    }
}
