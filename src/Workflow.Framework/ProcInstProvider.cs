using System;
using System.Collections.Generic;
using Workflow.DataContract.K2;
using Workflow.DataObject.Worklists;
using Workflow.K2Service;

namespace Workflow.Framework
{
    public class ProcInstProvider : IProcInstProvider
    {
        private string _currentUser;
        
        public ProcInstProvider(string currentUser) {
            _currentUser = currentUser;
        }
        
        public bool CanStartWorkFlow(string user, string procName)
        {
            using (var _server = new K2Server()) {
                return _server.CanStartWorkFlow(user, procName);
            }
        }

        public bool DoRedirect(string serial, DestinationDto user)
        {
            bool result = false;
            using (var client = new K2Client(_currentUser)) {
                client.Assign(serial, user.LoginName);
                result = true;
            }
            return result;
        }

        public void DoRelease(string serial)
        {
            using (var client = new K2Client(_currentUser))
            {
                client.Release(serial);
            }
        }

        public bool DoShare(string serial, IList<DestinationDto> users)
        {
            bool result = false;
            using (var client = new K2Client(_currentUser))
            {
                var destUsers = new List<string>();
                users.Each(p => {
                    destUsers.Add(p.LoginName.FQNWithK2Label());
                });
                client.Share(serial, destUsers);
                result = true;
            }
            return result;
        }

        public void Execute(ExecInstParam instances)
        {
            using (var client = new K2Client(_currentUser))
            {
                var dataFields = instances.DataFields;
                client.ExecuteWorklistItem(instances.SerialNo, instances.Action, dataFields);
            }
        }

        public void Execute(ExecInstParam instances, string sharedUser, string managedUser)
        {
            using (var client = new K2Client(_currentUser))
            {
                client.ExecuteWorklistItem(instances.SerialNo, instances.Action);
            }
        }

        public OOFWrapper GetOutOffice()
        {
            var criteria = new OOFWrapper();
            using (var client = new K2Client(_currentUser))
            {
                criteria = client.GetOOFCriteria();
            }
            //return criteria;
            return criteria;
        }

        public Participants GetParticipants(int procInstId, int actInstId)
        {
            var participants = new object();
            using (var client = new K2Client(_currentUser))
            {
                participants = client.GetParticipantsActInst(procInstId, actInstId);
            }
            return participants.TypeAs<Participants>();
        }

        public List<string> GetSharedUsers(string serial)
        {
            var sharedUsers = new List<string>();
            using (var client = new K2Client(_currentUser))
            {
                sharedUsers = client.GetSharedUsersBySerialNumber(serial);
            }
            return sharedUsers;
        }

        public string GetViewProcess(int procInstId)
        {
            var diagram = string.Empty;
            using (var client = new K2Client(_currentUser))
            {
                diagram = client.GetFlowDiagram(procInstId);
            }
            return diagram;
        }

        public List<Worklist> GetWorklist()
        {
            var worklist = new List<Worklist>();
            using (var client = new K2Client(_currentUser))
            {
                worklist = client.GetWorklist();
            }
            return worklist;
        }

        public ProcInst OpenWorklistItem(int proctInstId)
        {
            var procInst = new ProcInst();
            using (var client = new K2Client(_currentUser))
            {
                procInst = client.GetProcInstById(proctInstId);
            }
            return procInst;
        }

        public ProcInst OpenWorklistItem(string serialNo)
        {
            var procInst = new ProcInst();
            
            using (var client = new K2Client(_currentUser))
            {
                procInst = client.GetProcInstBySerial(serialNo);
            }
            return procInst;
        }

        public bool ReleaseWorklistItem(string serialNo)
        {
            bool result = false;
            using (var client = new K2Client(_currentUser))
            {
                client.Release(serialNo);
                result = true;
            }
            return result;
        }

        public bool RetryError(int procInstID, int procId = 0)
        {
            using (var _server = new K2Server())
            {
                return _server.RetryError(procInstID, procId);
            }
        }

        public bool SetOutOffice(OOFWrapper wrapper)
        {
            bool result = false;
            using (var client = new K2Client(_currentUser))
            {
                client.SetOOFCriteria(wrapper);
                result = true;
            }
            return result;
        }

        public int StartProcInstance(InstParam instance)
        {
            int result = 0;
            using (var client = new K2Client(_currentUser))
            {
                result = client.StartProcessInstance(
                    instance.ProcName,
                    instance.Folio, 
                    instance.DataFields, 
                    Convert.ToInt16(instance.Priority));
            }
            return result;
        }

        public List<string> StartWorkflowList()
        {
            using (var _server = new K2Server())
            {
                return _server.GetProcessListByUser(_currentUser);
            }
        }
    }
}
