using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.DataObject.Dashboard;

namespace Workflow.DataAcess.Repositories.Interfaces {
    public interface ITaskRepository {
        ResourceWrapper GetTasksByLoginName(TaskQueryParameter queryParameter);
    }
}
