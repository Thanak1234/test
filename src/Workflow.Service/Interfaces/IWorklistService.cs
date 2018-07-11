using System.Collections.Generic;
using System.Threading.Tasks;
using Workflow.DataContract.K2;
using Workflow.DataObject;
using Workflow.DataObject.WM;
using Workflow.DataObject.Worklists;

namespace Workflow.Service.Interfaces
{
    public interface IWorklistService {
        ProcInst GetWorklistItem(int proctInstId);
        ResourceWrapper GetWorklists(WMQueryParameter parameter);
        Task<ResourceWrapper> GetWorklistWrapper();
        List<WorkflowDto> GetWorkflows();
        List<ProcessDto> GetProcesses();
        List<ActivityDto> GetActivities();
        OOFWrapper GetShareWorklist();
        bool SetOutOfOffice(OOFWrapper wrapper);
        IEnumerable<DestinationDto> GetSharedUsers(string sn);
        bool SetSharedUsers(string sn, IList<DestinationDto> users, Domain.Entities.ActivityHistory actLog);
        bool Redirect(string serialNumber, DestinationDto user, Domain.Entities.ActivityHistory actLog);
        void Release(string serialNumber);
        void ExecuteAction(string serialNumber, string action, string sharedUser, string managedUser);
        object RunTest(string param);
        ResourceWrapper GetProcInstAudits(int procInstId);
        byte[] GetImageStream(int procInstId);
        object GetApprovers(int procInstId);
        object GetParticipants(int procInstId, int actId);
        object GetJson(int procInstId);
        bool RetryError(int procInstID, int procId = 0);
    }
}
