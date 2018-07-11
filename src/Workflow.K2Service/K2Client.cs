using SourceCode.Workflow.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Workflow.DataContract.K2;
using Action = SourceCode.Workflow.Client.Action;
using Worklist = Workflow.DataContract.K2.Worklist;

namespace Workflow.K2Service
{
    public class K2Client : IK2Client, IDisposable
    {
        #region Data Memebers

        private readonly List<string> _commentKeys = new List<string>() { "ActionComments", "Comment", "UserComment", "User Comment" };

        private Connection _connection = null;

        public string ManagedUser { set; get; }

        public string AllocatedUser { set; get; }

        #endregion

        #region Contructors

        public K2Client(string identity)
        {
            this.ManagedUser = identity.FQNWithK2Label();

            if (_connection == null) {
                var setup = new ConnectionSetup();
                var setting = ConfigurationManager.ConnectionStrings["WorkflowServer"];
                setup.ParseConnectionString(setting.ConnectionString);
                _connection = new Connection();
                _connection.Open(setup);
                if (_connection.User == null || string.Compare(_connection.User.Name, ManagedUser, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    _connection.ImpersonateUser(ManagedUser);
                }
            }
        }

        #endregion

        #region Worklist - Process Instance

        public ProcInst GetProcInstById(int proctInstId)
        {
            return GetProcInstBySerial(string.Format("{0}_0", proctInstId));
        }

        public WorklistItem GetWorklistItemBySerial(string serialNo)
        {
            return GetProcInst(serialNo.ToProcInstId(), serialNo.ToActDestInstId());
        }

        public List<string> GetSharedUsersBySerialNumber(string serialNo)
        {
            var item = GetWorklistItemBySerial(serialNo);
            var users = new List<string>();
            if (item != null)
            {
                for (int i = 0; i < item.DelegatedUsers.Count; i++)
                {
                    users.Add(item.DelegatedUsers[i].Name.FQNWithoutK2Label());
                }
            }
            return users;
        }

        public ProcInst GetProcInstBySerial(string serialNumber)
        {

            string[] serials = serialNumber.Split('_');
            var K2WLItem = GetProcInst(Convert.ToInt16(serials[0]), Convert.ToInt16(serials[1]));
            if (K2WLItem == null)
            {
                return null;
            }

            var procInst = new ProcInst()
            {
                ActivityName = K2WLItem.ActivityInstanceDestination.Name,
                Serial = K2WLItem.SerialNumber,
                AllocatedUser = K2WLItem.AllocatedUser,
                Folio = K2WLItem.ProcessInstance.Folio,
                OpenedBy = K2WLItem.AllocatedUser,
                Status = K2WLItem.Status.ToString(),
                OpenFormUrl = K2WLItem.Data,
                ProcInstId = K2WLItem.ProcessInstance.ID
            };

            foreach (Action action in K2WLItem.Actions)
            {
                procInst.Actions.Add(action.Name);
            }

            return procInst;
        }

        #region Private Method : WorklistItem GetProcInst(int proctInstId, int actDestInstId = 0)
        
        private WorklistItem GetProcInst(int proctInstId, int actDestInstId = 0)
        {
            string serialNumber = string.Format("{0}_{1}", proctInstId.ToString(), actDestInstId.ToString());

            var K2Crit = new WorklistCriteria();

            K2Crit.NoData = true; //faster, does not return the data for each item 
            K2Crit.Platform = "ASP"; //helps when multiple platform are used 

            K2Crit.AddFilterField(
                WCLogical.Or, WCField.WorklistItemOwner, "Me",
                WCCompare.Equal, WCWorklistItemOwner.Me
            );

            K2Crit.AddFilterField(
                WCLogical.Or, WCField.WorklistItemOwner, "Other",
                WCCompare.Equal, WCWorklistItemOwner.Other
            );

            K2Crit.AddFilterField(
                WCLogical.And, WCField.SerialNumber,
                WCCompare.Equal, string.Format("{0}_{1}", proctInstId.ToString(), actDestInstId.ToString())
            );

            var K2WList = _connection.OpenWorklist(K2Crit);

            foreach (WorklistItem K2WLItem in K2WList)
            {
                if (actDestInstId > 0)
                {
                    if (K2WLItem.ProcessInstance.ID == proctInstId)
                    {
                        return K2WLItem;
                    }
                }
                else {
                    if (K2WLItem.ProcessInstance.ID == proctInstId && K2WLItem.AllocatedUser == ManagedUser)
                    {
                        return K2WLItem;
                    }
                }
                
            }
            return null;
        }



        public List<Worklist> GetWorklist()
        {
            var procInsts = new List<Worklist>();
            var K2Crit = new WorklistCriteria();

            K2Crit.NoData = true; //faster, does not return the data for each item 
            K2Crit.Platform = "ASP"; //helps when multiple platform are used 

            K2Crit.AddFilterField(
                WCLogical.Or, WCField.WorklistItemOwner, "Me",
                WCCompare.Equal, WCWorklistItemOwner.Me
            );

            K2Crit.AddFilterField(
                WCLogical.Or, WCField.WorklistItemOwner, "Other",
                WCCompare.Equal, WCWorklistItemOwner.Other
            );

            K2Crit.AddSortField(WCField.ActivityStartDate, WCSortOrder.Descending);

            try
            {
                var K2WList = _connection.OpenWorklist(K2Crit);
                foreach (WorklistItem item in K2WList)
                {
                    
                    var procInst = new Worklist()
                    {
                        ProcInstId = item.ProcessInstance.ID,
                        ActInstDestId = item.ActivityInstanceDestination.ID,
                        Serial = item.SerialNumber,
                        Folio = item.ProcessInstance.Folio,
                        WorkflowPath = item.ProcessInstance.FullName,
                        OpenFormUrl = item.Data,
                        Originator = item.ProcessInstance.Originator.Name,
                        ManagedUser = ManagedUser,
                        Priority = item.ProcessInstance.Priority,
                        AllocatedUser = item.AllocatedUser,
                        OpenedBy = (item.Status == WorklistStatus.Allocated) ? item.AllocatedUser : string.Empty,
                        Status = (item.Status == WorklistStatus.Allocated) ? WorklistStatus.Open.ToString() : WorklistStatus.Available.ToString(),
                        ActivityName = item.ActivityInstanceDestination.Name
                    };

                    foreach (Action action in item.Actions)
                    {
                        if (action.Batchable) {
                            procInst.Actions.Add(action.Name);
                        }
                    }

                    var existed = procInsts.SingleOrDefault(p => p.ProcInstId == procInst.ProcInstId);
                    bool skip = false;
                    if (existed != null)
                    {
                        skip = (existed.AllocatedUser == ManagedUser);
                        // Removed duplicated item for OOF or Shared
                        if (!skip)
                        {
                            procInsts.Remove(existed);
                        }
                    }
                    if (!skip)
                    {
                        procInsts.Add(procInst);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return procInsts;
        }

        #endregion

        #endregion

        #region Public Method

        public int StartProcessInstance(string procFullPath, string folio, IDictionary<string, object> dataFields, int priority)
        {
            if (string.IsNullOrEmpty(procFullPath) ||
                string.IsNullOrEmpty(folio)) { return 0; }

            var procInst = _connection.CreateProcessInstance(procFullPath);
            string commentKey = null;
            procInst.Folio = folio;
            procInst.Priority = priority;

            foreach (DataField dataField in procInst.DataFields)
            {
                commentKey = _commentKeys.SingleOrDefault(p => p == dataField.Name);
                if (commentKey != null)
                {
                    break;
                }
            }

            if (dataFields != null && dataFields.Count > 0)
            {
                List<string> keys = dataFields.Keys.ToList();
                keys.ForEach((key) =>
                {
                    if (commentKey != null && _commentKeys.Contains(key))
                    {
                        procInst.DataFields[commentKey].Value = dataFields[key];
                    }
                    else
                    {
                        procInst.DataFields[key].Value = dataFields[key];
                    }
                });
            }
            _connection.StartProcessInstance(procInst);

            return procInst.ID;
        }

        public void ExecuteWorklistItem(string serialNo, string actionName, IDictionary<string, object> dataFields = null)
        {
            var item = GetWorklistItemBySerial(serialNo);
            if (item == null || item.Actions[actionName] == null)
            {
                return;
            }

            if (dataFields == null)
            {
                if (item != null && item.Actions[actionName] != null)
                {
                    item.Actions[actionName].Execute();
                }
            }

            string commentKey = string.Empty, comment = string.Empty;
            foreach (string key in _commentKeys)
            {
                comment = dataFields.SingleOrDefault(p => p.Key == key).Value?.ToString();
                dataFields.Remove(key);
                if (!string.IsNullOrEmpty(comment))
                {
                    break;
                }

            };

            if (!string.IsNullOrEmpty(comment))
            {
                foreach (DataField dataField in item.ProcessInstance.DataFields)
                {
                    commentKey = _commentKeys.SingleOrDefault(p => p == dataField.Name);
                    if (!string.IsNullOrEmpty(commentKey))
                    {
                        item.ProcessInstance.DataFields[commentKey].Value = comment;
                        break;
                    }
                }
            }

            if (dataFields != null && dataFields.Count > 0)
            {
                List<string> keys = dataFields.Keys.ToList();

                keys.ForEach((key) =>
                {
                    item.ProcessInstance.DataFields[key].Value = dataFields[key];
                });
            }

            item.Actions[actionName].Execute();
        }
        #region OOF - Section

        public OOFWrapper GetOOFCriteria()
        {
            var config = new OutOfOfficeConfig(_connection);
            var wrapper = new OOFWrapper();

            wrapper.Status = config.GetOOFStatus();

            var shared = config.LoadOOF();

            if (shared == null || shared.Count == 0) return wrapper;

            var oof = shared[0];

            if (shared.Count == 1 && oof.WorkTypes != null && oof.WorkTypes.Count == 1)
            {
                var worktype = oof.WorkTypes[0];
                var destinations = worktype.Destinations;
                wrapper.EndDate = Convert.ToDateTime(oof.EndDate);
                wrapper.StartDate = Convert.ToDateTime(oof.StartDate);
                wrapper.ShareType = Convert.ToInt16(oof.ShareType);
                wrapper.WorkType = config.GetWorktypeDto(worktype);

                foreach (WorkTypeException w in worktype.WorkTypeExceptions)
                {
                    var processPath = Convert.ToString(w.WorklistCriteria.Filters[0].Value);
                    var act = Convert.ToString(w.WorklistCriteria.Filters[1].Value);

                    if (string.IsNullOrEmpty(processPath)) continue;
                    if (string.IsNullOrEmpty(act)) continue;

                    string activityKey = string.Format("{0}\\{1}", processPath, act);

                    var dto = new WorkTypeExceptionDto()
                    {
                        Name = w.Name,
                        Process = processPath,
                        ProcessPath = processPath,
                        Activity = act,
                        ActDisplayName = activityKey,
                        Destinations = config.GetDestinationDto(w.Destinations)
                    };
                    wrapper.WorkType.WorkTypeExceptions.Add(dto);
                }
            }
            return wrapper;

        }

        public bool SetOOFCriteria(OOFWrapper wrapper)
        {
            var config = new OutOfOfficeConfig(_connection);
            if (wrapper.WorkType == null)
            {
                 _connection.SetUserStatus(Convert.ToBoolean(wrapper.Status) ? UserStatuses.Available : UserStatuses.OOF);
                return false;
            }

            bool isNew = false;
            WorklistShares worklistShares = new WorklistShares();
            worklistShares = _connection.GetCurrentSharingSettings(ShareType.OOF);
            WorklistShare worklistShare = null;
            if (worklistShares.Count > 0)
            {
                worklistShare = worklistShares[0];
                worklistShare.ShareType = ShareType.OOF;
            }
            else
            {
                isNew = true;
            }

            if (worklistShare == null)
            {
                worklistShare = new WorklistShare();
                worklistShare.ShareType = ShareType.OOF;
                isNew = true;
            }


            worklistShare.StartDate = wrapper.StartDate;
            worklistShare.EndDate = wrapper.EndDate;

            WorkTypes workTypes = worklistShare.WorkTypes;
            WorkType workType = new WorkType();

            if (workTypes.Count > 0)
            {
                workType = workTypes[0];
            }
            else
            {
                workType = new WorkType();
                workTypes.Add(workType);
            }
            workType.Name = Guid.NewGuid().ToString();
            Destinations destinations = new Destinations();

            foreach (DestinationDto dest in wrapper.WorkType.Destinations)
            {
                var destination = new Destination(dest.LoginName.FQNWithK2Label(), DestinationType.User);
                destinations.Add(destination);
            }

            workType.Destinations = destinations;
            workType.WorkTypeExceptions = config.GetWorkTypeExceptions(wrapper.WorkType.WorkTypeExceptions);
            worklistShare.WorkTypes = workTypes;

            if (isNew)
            {
                _connection.ShareWorkList(worklistShare);
            }
            _connection.UpdateWorkType(worklistShare.WorkTypes[0]);
            _connection.SetUserStatus(Convert.ToBoolean(wrapper.Status) ? UserStatuses.Available : UserStatuses.OOF);
            
            return true;
        } 

        #endregion

        public object GetParticipantsActInst(int procInstId, int actInstId)
        {
            ViewData viewData = new ViewData();
            viewData.ViewName = "ProcessView";
            viewData.Properties["Header"] = "GetActivityInstanceParticipantInfo";
            viewData.Properties["ActInstID"] = actInstId.ToString();
            viewData.Properties["UserID"] = ManagedUser;
            
            var xmlViewProcInst = _connection.ViewProcessInstance(procInstId, viewData);
            return xmlViewProcInst.XmlDeserializeFromString<Participants>();
        }

        public void Share(string serialNo, IList<string> destUsers)
        {
            var item = GetWorklistItemBySerial(serialNo);

            
            if (item == null || destUsers == null || destUsers.Count == 0)
            {
                throw new InvalidOperationException("SHARE-FN: parameter was not found.");
            }

            var destinations = new Destinations();
            destUsers.Each(p =>
            {
                destinations.Add(new Destination(p, DestinationType.User));
            });
            item.Delegate(destinations);
        }

        public void Assign(string serialNo, string destUser)
        {
            var item = GetWorklistItemBySerial(serialNo);
            if (item != null)
            {
                item.Redirect(destUser.FQNWithK2Label());
            }
        }

        public void Release(string serialNo)
        {
            var item = GetWorklistItemBySerial(serialNo);
            if (item != null)
            {
                item.Release();
            }
        }

        public string GetFlowDiagram(int procInstId)
        {
            ViewData viewData = new ViewData();
            viewData.Properties["definitionOnly"] = "false";
            return _connection.ViewProcessInstance(procInstId);
        } 
        #endregion

        public void Dispose()
        {
            ManagedUser = null;
            AllocatedUser = null;
            _connection.Close();
        }
    }
}
