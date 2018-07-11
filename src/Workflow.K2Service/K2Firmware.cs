namespace Workflow.K2Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Workflow.Domain;
    using Workflow.Domain.Domain;
    using Workflow.K2Service;
    using Workflow.K2Service.Cores;
    using Workflow.Dto.Worklists;
    using K2 = SourceCode.Workflow.Client;
    using Workflow.Dto;
    using System.Data.SqlClient;
    using System.Collections;
    using SourceCode.Workflow.Runtime;
    using System.Configuration;

    public class K2Firmware : IK2Firmware
    {

        /// <summary>
        /// Custome user name to get worklist
        /// </summary>
        /// 
        protected string _userAccount;

        protected string _smartFormUrl;

        protected IK2Client _client;

        protected IK2Server _k2Server;

        public K2Firmware(string userAccount = null)
        {
            if (!string.IsNullOrEmpty(userAccount))
                _userAccount = userAccount;

            _client = new BlackpearlClient(_userAccount);
            _k2Server = new K2Server();
            _smartFormUrl = ConfigurationManager.AppSettings["SmartFormUrl"] ?? "https://forms.nagaworld.com/";
        }

        #region Move To K2 Service

        public Domain.K2.Participants GetParticipants(int procInstId, int actInstId)
        {
            return _client.GetParticipantsActInst(procInstId, actInstId);
        }

        public string GetViewProcess(int procInstId)
        {
            return _client.GetViewProcess(procInstId);
        }

        private string BuildCriteria(K2.WorklistCriteria criteria, bool reverse)
        {

            if (criteria == null && criteria.Filters.Count() > 0)
                return string.Empty;

            string filter = "";

            foreach (K2.WCFilter f in criteria.Filters)
            {
                filter += string.Format("{0}{1}{2}", f.Field, f.Logical, f.Value);
            }

            return string.Empty;
        }

        public void Load(K2.WorklistCriteria criteria, ArchiveX ax)
        {
            criteria.Platform = ax.GetString();
            criteria.ManagedUser = ax.GetString();
            criteria.StartIndex = ax.GetInt32();
            criteria.Count = ax.GetInt32();
            int temp = ax.GetInt32();
            if (ax.Version >= 5)
            {
                criteria.NoData = ax.GetBool();
            }
            if (temp > 0 && 0 < temp)
            {
                int num = temp;
                do
                {
                    criteria.AddFilterField((K2.WCLogical)ax.GetByte(), (K2.WCField)ax.GetByte(), ax.GetString(), (K2.WCCompare)ax.GetByte(), ax.GetObject());
                    num--;
                }
                while (num != 0);
            }
            int int2 = ax.GetInt32();
            if (int2 > 0 && 0 < int2)
            {
                int num2 = int2;
                do
                {
                    criteria.AddSortField((K2.WCField)ax.GetByte(), ax.GetString(), (K2.WCSortOrder)ax.GetByte());
                    num2--;
                }
                while (num2 != 0);
            }
        }

        public Domain.K2.WorklistItem GetWorklistItem(int procInstId)
        {

            WorklistItemCollection worklist = _client.OpenWorklist();
            if (worklist != null && worklist.Count > 0)
            {
                var wItem = worklist.SingleOrDefault(p => p.ProcessInstance.ID == procInstId);

                return new Domain.K2.WorklistItem()
                {
                    SerialNumber = wItem.SerialNumber,
                    AllocatedUser = wItem.AllocatedUser,
                    OpenBy = wItem.OpenBy
                };
            }

            return null;
        }

        public List<WorklistDto> GetWorklist()
        {
            var tasks = new List<WorklistDto>();
            var worklists = _client.OpenWorklist();

            if (worklists.Count == 0)
            {
                return tasks;
            }

            worklists.Each((worklistItem) =>
            {
                var task = new WorklistDto()
                {
                    ProcInstId = worklistItem.ProcessInstance.ID,
                    Serial = worklistItem.SerialNumber,
                    Originator = worklistItem.ProcessInstance.Name,
                    Folio = worklistItem.ProcessInstance.Folio,
                    WorkflowName = worklistItem.ProcessInstance.Name,
                    ActivityName = worklistItem.ActivityInstanceDestination.Name,
                    StartDate = worklistItem.ProcessInstance.StartDate,
                    ActivityStartDate = worklistItem.ActivityInstanceDestination.StartDate,
                    Priority = worklistItem.ProcessInstance.Priority,
                    Status = worklistItem.Status.ToString(),
                    AllocatedUser = worklistItem.AllocatedUser,
                    OpenedBy = string.Empty,
                    SharedUser = worklistItem.AllocatedUser ?? string.Empty,
                    ManagedUser = _client.GetFQN(),
                    IsShared = (worklistItem.AllocatedUser != null && worklistItem.AllocatedUser.Length > 0),
                    Actions = worklistItem.Actions.Where(p => p.Batchable == true).ToList(),
                    NoneK2Form = true
                };

                tasks.Add(task);
            });

            return tasks;
        }
        
        public List<WorklistDto> GetWorklist(IEnumerable<WorklistItemDto> items)
        {
            var worklists = new List<WorklistDto>();

            WorklistDto view = new WorklistDto();

            int processId = 0;
            int actInstId = 0;

            foreach (WorklistItemDto item in items)
            {
                if (_userAccount.Equals(item.Originator, StringComparison.OrdinalIgnoreCase) && !_userAccount.Equals(item.ActionerName, StringComparison.OrdinalIgnoreCase))
                    continue;
                
                if ((item.ProcessInstanceID != processId) || (item.ProcessInstanceID == processId && actInstId != item.ActInstDestID))
                {
                    actInstId = item.ActInstDestID;
                    processId = item.ProcessInstanceID;
                    view = new WorklistDto()
                    {
                        RequestorId = item.EmpNo,
                        WorkflowName = item.Workflow,
                        Requestor = item.EmpName,
                        Folio = item.Folio,
                        ActivityName = item.ActivityName,
                        ViewFlow = item.ViewFLow,
                        ViewFromUrl = item.ViewUrl,
                        IsShared = !string.IsNullOrEmpty(item.SharedUser),
                        NoneK2Form = item.NoneK2,
                        Serial = item.SeriaNumber,
                        SharedUser = item.SharedUser,
                        ManagedUser = item.ManagedUser,
                        OpenedBy = item.OpenedBy ?? string.Empty,
                        Data = item.OpenUrl,
                        Status = ((K2.WorklistStatus)item.Status).ToString(),
                        ActivityStartDate = item.TaskStartDate,
                        Actions = new ActionCollection(),
                        AllocatedUser = item.ActionerName,
                        Priority = Convert.ToInt16(item.Priority),
                        Originator = item.Originator
                    };

                    worklists.Add(view);
                }

                if (item.Status == (int)K2.WorklistStatus.Available || item.Status == (int)K2.WorklistStatus.Sleep || item.Status == (int)K2.WorklistStatus.Open)
                {

                    if (item.Denied == true || item.Execute == false) continue;

                    var actions = view.Actions as ActionCollection;
                    var act = new Cores.Action();
                    act.Batchable = item.Batchable;
                    act.Name = item.ActionName;
                    actions.Add(act);
                }
            }

            return worklists;
        }

        #endregion

        public OOFWrapper GetOutOffice()
        {

            var wrapper = new OOFWrapper();

            wrapper.Status = _client.GetOOFStatus();

            var shared = _client.LoadOOF();

            if (shared == null || shared.Count == 0) return wrapper;

            var oof = shared[0];

            if (shared.Count == 1 && oof.WorkTypes != null && oof.WorkTypes.Count == 1)
            {

                var worktype = oof.WorkTypes[0];
                var destinations = new List<DestinationDto>();
                wrapper.EndDate = Convert.ToDateTime(oof.EndDate);
                wrapper.StartDate = Convert.ToDateTime(oof.StartDate);
                wrapper.ShareType = Convert.ToInt16(oof.ShareType);

                for (int i = 0; i < worktype.Destinations.Count; i++) {
                    destinations.Add(new DestinationDto()
                    {
                        LoginName = worktype.Destinations[i].Name
                    });
                } 
                
                wrapper.WorkType = new WorkTypeDto()
                {
                    WorklistCriteria = GetWorklistCriteriaDto(worktype.WorklistCriteria),
                    Name = worktype.Name,
                    Destinations = destinations
                };
                
                foreach (K2.WorkTypeException w in worktype.WorkTypeExceptions)
                {
                    var processPath = Convert.ToString(w.WorklistCriteria.Filters[0].Value);
                    var act = Convert.ToString(w.WorklistCriteria.Filters[1].Value);

                    if (string.IsNullOrEmpty(processPath)) continue;
                    if (string.IsNullOrEmpty(act)) continue;
                    
                    wrapper.WorkType.WorkTypeExceptions.Add(new WorkTypeExceptionDto()
                    {
                        Name = w.Name,
                        Process = null,
                        ProcessPath = processPath,
                        Activity = act,
                        ActDisplayName = null,
                        DestUsers = new List<string>()
                    });
                }

            }

            return wrapper;
        }



        private string GetDestinationsList(List<DestinationDto> destinations)
        {
            ArrayList builder = new ArrayList();
            foreach (var dest in destinations)
            {
                builder.Add(dest.DisplayName.Trim());
            }
            return string.Join(", ", builder.ToArray());
        }


        private WorklistCriteriaDto GetWorklistCriteriaDto(K2.WorklistCriteria worklistCriteria)
        {
            var dto = new WorklistCriteriaDto()
            {
                Filters = GetFiltersDto(worklistCriteria.Filters),
                Count = worklistCriteria.Count,
                ManagedUser = worklistCriteria.ManagedUser,
                NoData = worklistCriteria.NoData,
                Platform = worklistCriteria.Platform,
                StartIndex = worklistCriteria.StartIndex
            };

            return dto;
        }

        private List<FilterDto> GetFiltersDto(K2.WCFilter[] filters)
        {
            var dtos = new List<FilterDto>();

            foreach (K2.WCFilter wc in filters)
            {
                var dto = new FilterDto()
                {
                    Compare = Convert.ToInt16(wc.Compare),
                    Field = Convert.ToInt16(wc.Field),
                    Logical = Convert.ToInt16(wc.Logical),
                    SubField = wc.SubField,
                    Value = wc.Value
                };
                dtos.Add(dto);
            }

            return dtos;
        }

        public bool SetOutOffice(OOFWrapper wrapper)
        {
            return _client.SetOutOfOffice(wrapper);
        }

        public List<string> GetSharedUsers(string sn)
        {
            List<string> destUsers = new List<string>();
            K2.WorklistItem wItem = null;
            try
            {
                wItem = _client.GetWorklistItem(sn);
                
                if (wItem == null)
                {
                    throw new System.InvalidOperationException(string.Format("Not Found", sn));
                }

                var delegateUsers = wItem.DelegatedUsers;

                if (delegateUsers != null && delegateUsers.Count > 0)
                {

                    foreach (K2.Destination dest in delegateUsers)
                    {

                        if (dest.Name.IsCaseInsensitiveEqual(_userAccount)) continue;

                        var criteria = K2Service.Cores.SecurityLabelUtils.GetNameWithoutLabel(dest.Name).ToLower();

                        destUsers.Add(criteria);

                    }
                }
                wItem.Release();

                return destUsers;
            }
            catch (Exception)
            {
                return destUsers;
            }
        }

        public bool DoShare(string sn, IList<DestinationDto> users)
        {
            if (users == null || users.Count == 0) return false;
            IList<string> destinationUsers = new List<string>();

            foreach (var dest in users)
            {
                if (string.IsNullOrEmpty(dest.LoginName)) continue;
                var userAccount = SecurityLabelUtils.GetNameWithLabel(dest.LoginName);
                destinationUsers.Add(userAccount);
            }

            _client.Share(sn, destinationUsers);

            return true;
        }

        public bool DoRedirect(string serialNumber, DestinationDto user)
        {
            if (user == null || string.IsNullOrEmpty(user.LoginName) || !SecurityLabelUtils.IsCorrectUserName(user.LoginName)) return false;
            _client.RedirectWorklistItem(serialNumber, SecurityLabelUtils.GetNameWithLabel(user.LoginName));
            return true;
        }

        public void DoRelease(string serialNumber)
        {
            _k2Server.ReleaseWorklistItem(serialNumber);
        }

        public WorklistDto GetTaskByProcInstId(int procInstId)
        {
            try
            {
                using (var workflowClient = _client.GetWorkflowClient())
                {
                    K2.WorklistCriteria wlc = new K2.WorklistCriteria();
                    wlc.AddFilterField(K2.WCField.ProcessID, K2.WCCompare.Equal, procInstId);

                    var results = workflowClient.OpenWorklist(wlc);
                    if (results.TotalCount > 0)
                    {
                        var workListItem = results[0];
                        if (workListItem.Status == K2.WorklistStatus.Allocated)
                        {
                            IK2Server blackpearServer = new K2Server();
                            bool isDone = blackpearServer.ReleaseWorklistItem(workListItem.SerialNumber);
                            if (isDone)
                            {
                                results = workflowClient.OpenWorklist(wlc);
                                workListItem = results[0];
                            }
                        }
                        var workListItemDto = new WorklistDto()
                        {
                            Serial = workListItem.SerialNumber,
                            AllocatedUser = workListItem.AllocatedUser,
                            Data = workListItem.Data,
                            Folio = workListItem.ProcessInstance.Folio,
                            ActivityName = workListItem.ActivityInstanceDestination.Name,
                            Status = ((K2.WorklistStatus)workListItem.Status).ToString()
                        };
                        return workListItemDto;
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
