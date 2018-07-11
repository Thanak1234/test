using SourceCode.Security.UserRoleManager.Management;
using SourceCode.Workflow.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workflow.DataContract.K2;

namespace Workflow.K2Service {
    public interface IK2Server {
        List<string> GetProcessListByUser(string loginName);
        bool CanStartWorkFlow(string loginName, string procFullPath);
        WorklistItems GetWorklistItemsByProcInstId(int procInstId);
        bool ReleaseWorklistItem(string serialNo);
        bool RetryError(int procInstId, int procId = 0);
    }
}
