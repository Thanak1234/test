using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {
    public enum ProcessStatus {
        Error,
        Running,
        Active,
        Completed,
        Stopped,
        Deleted,
        New
    }
}
