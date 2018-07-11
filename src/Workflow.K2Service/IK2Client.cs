using SourceCode.Workflow.Client;
using System.Collections.Generic;
using Workflow.DataContract.K2;

namespace Workflow.K2Service
{
    public interface IK2Client
    {
        /* Process Instance and Worklist Item */
        int StartProcessInstance(string procFullPath, string folio, IDictionary<string, object> dataFields, int priority);
        WorklistItem GetWorklistItemBySerial(string serialNumber);
        ProcInst GetProcInstBySerial(string serialNo);
        ProcInst GetProcInstById(int proctInstId);
        List<DataContract.K2.Worklist> GetWorklist();

        void ExecuteWorklistItem(string serialNo, string actionName, IDictionary<string, object> dataFields = null);
        object GetParticipantsActInst(int procInstId, int actInstId);
        string GetFlowDiagram(int procInstId);

        /* Release/Share/Redirect */
        List<string> GetSharedUsersBySerialNumber(string loginName = null);
        void Share(string serialNo, IList<string> destUsers);

        OOFWrapper GetOOFCriteria();
        bool SetOOFCriteria(OOFWrapper criteria);

        void Assign(string serialNo, string destUser);
        void Release(string serialNo);

        
    }
}
