using SourceCode.Security.UserRoleManager.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.K2Service {

    public interface IConnectionFactory {
        UserRoleManager GetRoleManager();
    }
}
