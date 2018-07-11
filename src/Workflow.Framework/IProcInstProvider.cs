using System.Collections.Generic;
using Workflow.DataContract.K2;
using System.Threading.Tasks;
using Workflow.DataObject.Worklists;
using Workflow.Domain.Entities.Core;

namespace Workflow.Framework
{
    public interface IProcInstProvider
    {
        bool CanStartWorkFlow(string user, string procFullPath);
        int StartProcInstance(InstParam instance); // return ProcInstId
        ProcInst OpenWorklistItem(string serialNo);
        ProcInst OpenWorklistItem(int procInstId);
        List<Worklist> GetWorklist();

        void Execute(ExecInstParam instances);
        void Execute(ExecInstParam instances, string sharedUser, string managedUser);
        bool ReleaseWorklistItem(string serialNo);
        List<string> StartWorkflowList();
        bool RetryError(int procInstID, int procId = 0);

        string GetViewProcess(int procInstId);
        Participants GetParticipants(int procInstId, int actInstId);

        OOFWrapper GetOutOffice();
        bool SetOutOffice(OOFWrapper wrapper);
        List<string> GetSharedUsers(string serial);
        bool DoShare(string serial, IList<DestinationDto> users);
        bool DoRedirect(string serial, DestinationDto user);
        void DoRelease(string serial);
    }
}
