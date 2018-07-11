using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataContract.K2;
using Workflow.DataObject;
using Workflow.DataObject.WM;
using Workflow.DataObject.Worklists;
using Workflow.Domain.Entities.Core;

namespace Workflow.DataAcess.Repositories.Interfaces {
    public interface IWMRepository {
        IEnumerable<InstanceAuditDto> GetProcInstAudits(int procInstId);
        string GetDisplayNameByUser(string userName);
        IDictionary<string, string> GetPairLoginDisplayName(string[] users);
        List<Worklist> GetDbWorklist(string account);
        ResourceWrapper GetWorklists(WMQueryParameter parameter);
        List<WorklistHeader> GetWorklistHeader(string[] procInstIds);
        List<WlCriteriaDto> GetWorklistByUser(string userName);
        RequestApplication GetReqAppByCode(string code);
        List<WorkflowDto> GetWorkflows();
        List<ProcessDto> GetProcesses();
        List<ActivityDto> GetActivitiesByReqCode(string reqCode);
        List<ActivityDto> GetActivities();
        List<DestinationDto> GetDestinationDto(string[] userNames);

        Dictionary<string, dynamic> GetProcessDictionary();
        Dictionary<string, string> GetActivitiesDictionary();
    }
}
