using SourceCode.Workflow.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workflow.Domain.K2;
using Workflow.Dto.Worklists;
using Workflow.Domain.Domain;

namespace Workflow.K2Service
{
    public interface IK2Firmware {
        ProcInst GetWorklistItem(int procInstId);
        List<ProcInst> GetWorklist();
        ProcInst GetTaskByProcInstId(int procInstId);
        
        string GetViewProcess(int procInstId);
        Participants GetParticipants(int procInstId, int actInstId);

        OOFWrapper GetOutOffice();
        bool SetOutOffice(OOFWrapper wrapper);
        List<string> GetSharedUsers(string serial);
        bool DoShare(string serial, IList<DestinationDto> users);
        bool DoRedirect(string serial, DestinationDto user);
        void DoRelease(string serial);
        List<WorklistDto> GetWorklist(IEnumerable<WorklistItemDto> worklist);
    }
}