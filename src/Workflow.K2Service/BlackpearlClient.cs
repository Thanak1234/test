using SourceCode.Workflow.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Workflow.Domain.K2;
using Workflow.Dto.Worklists;
using Workflow.K2Service.Cores;
using K2 = SourceCode.Workflow.Client;

namespace Workflow.K2Service
{
    public class BlackpearlClient
    {
        #region Data Memebers

        private readonly IList<string> _comments = new List<string>() {
            "ActionComments", "Comment", "UserComment", "User Comment"
        };

        private string _userAccount { set; get; }

        private IK2Server _server;

        #endregion

        #region Contructors

        public BlackpearlClient() { }

        public BlackpearlClient(string identity)
        {
            _server = new K2Server();
            this._userAccount = identity.FQNWithK2Label();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Static method for creating instance  
        /// </summary>
        /// <returns>The Blackpearl client instance</returns>
        public static IK2Client CreateClient() {
            return new BlackpearlClient();
        }

        /// <summary>
        /// Get user full qualify name
        /// </summary>
        /// <returns>The FQN</returns>
        public string GetFQN() {
            using(Connection client = GetWorkflowClient()) {
                return client.User.FQN;
            }
        }

        public User GetUser() {
            using (Connection client = GetWorkflowClient()) {
                return client.User;
            }
        }

        /// <summary>
        /// Get Workflow Client with impersonate user
        /// </summary>
        /// <returns>The worklist connection to K2 Workflow server</returns>
        public Connection GetWorkflowClient()
        {
            ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["WorkflowServer"];

            if (_userAccount == null)
                return null;

            if (setting == null)
                return null;

            if (string.IsNullOrEmpty(setting.ConnectionString))
                return null;

            ConnectionSetup connectionSetup = new ConnectionSetup();
            connectionSetup.ParseConnectionString(setting.ConnectionString);
            Connection connection = new Connection();
            connection.Open(connectionSetup);
            if (connection.User == null || string.Compare(connection.User.Name, _userAccount, StringComparison.OrdinalIgnoreCase) != 0) {
                connection.ImpersonateUser(_userAccount);
            }
            return connection;
        }

        /// <summary>
        /// Get all worklist items of current user
        /// </summary>
        /// <returns>The worklist items</returns>
        public Worklist GetWorklists()
        {
            using(Connection conn = GetWorkflowClient())
            {
                return conn.OpenWorklist();
            }
        }

        public string GetViewProcess(int procInstId) {
            using (Connection conn = GetWorkflowClient()) {
                ViewData viewData = new ViewData();
                viewData.Properties["definitionOnly"] = "false";
                return conn.ViewProcessInstance(procInstId);
            }
        }

        public Participants GetParticipantsActInst(int procInstId, int actInstId) {
            using (Connection conn = GetWorkflowClient()) {
                ViewData viewData = new ViewData();
                viewData.ViewName = "ProcessView";
                viewData.Properties["Header"] = "GetActivityInstanceParticipantInfo";
                viewData.Properties["ActInstID"] = actInstId.ToString();
                viewData.Properties["UserID"] = _userAccount;
                var participants = conn.ViewProcessInstance(procInstId, viewData);
                return participants.XmlDeserializeFromString<Participants>();
            }
        }

        /// <summary>
        /// To execute Worklist Item action by serial number and action name
        /// </summary>
        /// <param name="sn">The worklist item serial number</param>
        /// <param name="actionName">The Action name</param>
        public void ExecuteWorklistItem(string sn, string actionName, string sharedUser, string managedUser)
        {
            using (Connection conn = GetWorkflowClient())
            {
                K2.WorklistItem item = conn.OpenWorklistItem(sn, "ASP", false, false, managedUser, sharedUser);
                if (item == null || item.Actions[actionName] == null)
                    return;

                item.Actions[actionName].Execute();
            }
        }

        /// <summary>
        /// Get worklist items by worklist filter criterias
        /// </summary>
        /// <returns>The collection of worklist items</returns>
        public WorklistItemCollection OpenWorklist() {

            WorklistItemCollection result;
            try {
                using (Connection workflowClient = this.GetWorkflowClient()) {                    
                    WorklistCriteria criteria = new WorklistCriteria();
                    criteria.Platform = "ASP";
                    criteria.NoData = true;
                    criteria.AddFilterField(WCLogical.Or, WCField.WorklistItemOwner, "Me", WCCompare.Equal, WCWorklistItemOwner.Me);
                    criteria.AddFilterField(WCLogical.Or, WCField.WorklistItemOwner, "Other", WCCompare.Equal, WCWorklistItemOwner.Other);
                    criteria.AddSortField(WCField.EventStartDate, WCSortOrder.Descending);
                    result = OpenWorklist(criteria);
                }
            } catch (UnauthorizedAccessException) {
                throw;
            } catch (SmartException ex) {
                throw ex;
            }
            return result;
        }

        public string GetSharedUsersBySerialNumber(string serialNumber) {
            WorklistItemCollection result;
            try {
                using (Connection workflowClient = this.GetWorkflowClient()) {
                    WorklistCriteria criteria = new WorklistCriteria();
                    criteria.Platform = "ASP";
                    criteria.NoData = true;
                    criteria.AddFilterField(WCLogical.Or, WCField.WorklistItemOwner, "Me", WCCompare.Equal, WCWorklistItemOwner.Me);
                    criteria.AddFilterField(WCLogical.Or, WCField.WorklistItemOwner, "Other", WCCompare.Equal, WCWorklistItemOwner.Other);
                    criteria.AddFilterField(WCLogical.And, WCField.SerialNumber, WCCompare.Equal, serialNumber);
                    result = OpenWorklist(criteria);
                }
            } catch (UnauthorizedAccessException) {
                throw;
            } catch (SmartException ex) {
                throw ex;
            }
            return result.Count > 0 ? result[0].AllocatedUser: null;
        }

        /// <summary>
        /// Get worklist items by worklist filter criterias
        /// </summary>
        /// <param name="wlc">The worklist criteria</param>
        /// <returns>The collection of worklist items</returns>
        public WorklistItemCollection OpenWorklist(WorklistCriteria wlc) {
            WorklistItemCollection result;
            try {
                using (Connection workflowClient = this.GetWorkflowClient()) {
                    result = WorklistItemCollection.FromApi(workflowClient.OpenWorklist(wlc));
                }
            } catch (UnauthorizedAccessException) {
                throw;
            } catch (SmartException ex) {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Get worklist item by worklist filter criterias
        /// </summary>
        /// <param name="filter">The worklist filter criterias</param>
        /// <param name="actDataField">The activity data field</param>
        /// <param name="actXmlField">The activity xml field</param>
        /// <param name="piDataField">The pi data field</param>
        /// <param name="piXmlField">The pi xml field</param>
        /// <returns></returns>
        public WorklistItemCollection OpenWorklistFiltered(Criteria filter, bool actDataField, bool actXmlField, bool piDataField, bool piXmlField) {
            WorklistItemCollection result;
            try {
                using (Connection workflowClient = this.GetWorkflowClient()) {
                    WorklistCriteria criteria = filter.ToApi();
                    result = WorklistItemCollection.FromApi(workflowClient.OpenWorklist(criteria));
                }
            } catch (System.UnauthorizedAccessException ex) {
                throw ex;
            } catch (System.Exception ex) {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Call and execute worklist item's action
        /// </summary>
        /// <param name="serialNumber">The worklist item's serial number</param>
        /// <param name="action">The worklist item's action name</param>
        /// <param name="synchronous">The execution by synchronus or asynchronous</param>
        /// <param name="sharedUser">The shared user</param>
        /// <param name="managedUser">The manager user</param>
        public void ExecuteWorklistItem(string serialNumber, string action, bool synchronous, string sharedUser, string managedUser) {
            try {
                this.ExecuteAction(serialNumber, action, synchronous, true, null, sharedUser, managedUser);
            } catch (UnauthorizedAccessException ex) {
                throw ex;
            } catch (SmartException ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Remove worklist item's action name prefix
        /// </summary>
        /// <param name="value">The full action name with prefix</param>
        /// <returns>The actual action name</returns>
        private static string RemovePrefix(string value) {
            int num = value.IndexOf(':');
            return value.Substring(num + 1);
        }

        /// <summary>
        /// Call and execute worklist item's action
        /// </summary>
        /// <param name="serialNumber">The worklist item's serial number</param>
        /// <param name="action">The worklist item's action name</param>
        /// <param name="synchronous">The execution by synchronus or asynchronous</param>
        /// <param name="allowNonBatch">The allow non batch</param>
        /// <param name="item">The worklist item</param>
        private void ExecuteAction(string serialNumber, string action, bool synchronous, bool allowNonBatch, Cores.WorklistItem item) {
            this.ExecuteAction(serialNumber, action, synchronous, allowNonBatch, item, string.Empty, string.Empty);
        }

        /// <summary>
        /// Call and execute worklist item's action
        /// </summary>
        /// <param name="serialNumber">The worklist item's serial number</param>
        /// <param name="action">The worklist item's action name</param>
        /// <param name="synchronous">The execution by synchronus or asynchronous</param>
        /// <param name="allowNonBatch">The allow non batch</param>
        /// <param name="item">The worklist item</param>
        /// <param name="sharedUser">The shared users</param>
        /// <param name="managedUser">The manager user</param>
        private void ExecuteAction(string serialNumber, string action, bool synchronous, bool allowNonBatch, Cores.WorklistItem item, string sharedUser, string managedUser) {
            
            string text = action.ToUpperInvariant();
            if (text.StartsWith("A:") || text.StartsWith("ACTION:")) {
                action = RemovePrefix(action);
            } else {
                if (text.StartsWith("R:") || text.StartsWith("REDIRECT:")) {
                    this.Redirect(serialNumber, RemovePrefix(action));
                    return;
                }
                if (text.StartsWith("D:") || text.StartsWith("DELEGATE:")) {
                    this.DelegateWorklistItem(serialNumber, RemovePrefix(action));
                    return;
                }
            }
            using (Connection workflowClient = this.GetWorkflowClient()) {

                K2.WorklistItem worklistItem = this.OpenWorklistItem(workflowClient, serialNumber, sharedUser, managedUser);

                K2.Action newAction = worklistItem.Actions[action];

                if (newAction == null) {
                    throw new InvalidOperationException(string.Format("Not Found", action));
                }

                if (!newAction.Batchable && !allowNonBatch) {
                    throw new InvalidOperationException(string.Format("Not Batchable", action));
                }

                if (item != null) {
                    item.ToApi(worklistItem);
                }

                newAction.Execute(synchronous);
            }
        }

        /// <summary>
        /// Delegate or shared worklist to the specific user
        /// </summary>
        /// <param name="serialNumber">The serial number</param>
        /// <param name="destination">The specific user</param>
        public void DelegateWorklistItem(string serialNumber, string destination) {
            try {
                using (Connection workflowClient = this.GetWorkflowClient()) {
                    K2.WorklistItem worklistItem = workflowClient.OpenWorklistItem(serialNumber, null, false);
                    if (worklistItem == null) {
                        throw new InvalidOperationException(string.Format("Not Found", serialNumber));
                    }
                    K2.Destination destination2 = new K2.Destination();
                    foreach (K2.Action action in worklistItem.Actions) {
                        if (action != null) {
                            destination2.AllowedActions.Add(action.Name);
                        }
                    }
                    destination2.Name = destination;
                    destination2.DestinationType = DestinationType.User;
                    worklistItem.Delegate(destination2);
                }
            } catch (UnauthorizedAccessException ex) {
                throw ex;
            } catch (SmartException ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Delegate or shared worklist to other users
        /// </summary>
        /// <param name="serialNumber">The serial number</param>
        /// <param name="destinations">The other users</param>
        public void Share(string serialNumber, IList<string> destinations)
        {
            
            try
            {
                K2.WorklistItem worklistItem = GetWorklistItem(serialNumber);
                if (worklistItem == null)
                {
                    throw new InvalidOperationException(string.Format("Not Found", serialNumber));
                }

                if (destinations == null || destinations.Count == 0)
                    throw new InvalidOperationException(string.Format("Destination users is null or empty"));

                var destinationUsers = new K2.Destinations();
                foreach (string dest in destinations)
                {
                    K2.Destination destination = new K2.Destination();
                    foreach (K2.Action action in worklistItem.Actions)
                    {
                        if (action != null)
                        {
                            destination.AllowedActions.Add(action.Name);
                        }
                    }
                    destination.Name = dest;
                    destination.DestinationType = DestinationType.User;
                    destinationUsers.Add(destination);
                }

                worklistItem.Delegate(destinationUsers);
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// Open a worklist item by serial number
        /// </summary>
        /// <param name="con">The worklist connection</param>
        /// <param name="serialNumber">The serial number</param>
        /// <param name="sharedUser">The shared user</param>
        /// <param name="managedUser">The manager user</param>
        /// <returns>The worklist item</returns>
        public K2.WorklistItem OpenWorklistItem(Connection con, string serialNumber, string sharedUser, string managedUser) {
            K2.WorklistItem worklistItem = OpenWorklistItem(con, serialNumber, sharedUser, managedUser, true);
            return worklistItem;
        }

        /// <summary>
        /// Open a worklist item by serial number
        /// </summary>
        /// <param name="con">The worklist connection</param>
        /// <param name="serialNumber">The serial number</param>
        /// <param name="sharedUser">The shared user</param>
        /// <param name="managedUser">The manager user</param>
        /// <returns>The worklist item</returns>
        public K2.WorklistItem OpenWorklistItem(Connection con, string serialNumber, string sharedUser, string managedUser, bool alloc) {
            K2.WorklistItem worklistItem = null;
            if (sharedUser == string.Empty && managedUser == string.Empty) {
                worklistItem = con.OpenWorklistItem(serialNumber, "ASP", alloc);
            }
            if (sharedUser != string.Empty && managedUser == string.Empty) {
                worklistItem = con.OpenSharedWorklistItem(sharedUser, managedUser, serialNumber, "ASP", alloc);
            }
            if (sharedUser == string.Empty && managedUser != string.Empty) {
                worklistItem = con.OpenManagedWorklistItem(managedUser, serialNumber, "ASP", alloc);
            }
            if (sharedUser != string.Empty && managedUser != string.Empty) {
                worklistItem = con.OpenSharedWorklistItem(sharedUser, managedUser, serialNumber, "ASP", alloc);
            }
            if (worklistItem == null) {
                throw new InvalidOperationException(string.Format("Not Found", serialNumber));
            }
            return worklistItem;
        }

        
        /// <summary>
        /// Sleep worklist item by Serial Number
        /// </summary>
        /// <param name="serialNumber">The Serial Number of worklist item</param>
        /// <param name="hours">The durations in Hours</param>
        public void SleepWorklistItem(string serialNumber, int hours, string sharedUser, string managedUser) {
            try {
                using (Connection workflowClient = this.GetWorkflowClient()) {

                    K2.WorklistItem worklistItem = workflowClient.OpenWorklistItem(serialNumber, "ASP", false, false, managedUser, sharedUser);

                    if (worklistItem == null) {
                        throw new InvalidOperationException(string.Format("Not Found", serialNumber));
                    }

                    var duration = GetCalulateSeconds(hours);

                    if(duration <= 0)
                        worklistItem.Sleep(false);
                    else {
                        worklistItem.Sleep(true, duration);
                    }
                }
            } catch (SmartException ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Get calulate duration in seconds
        /// </summary>
        /// <param name="duration">The hours</param>
        /// <returns>The seconds</returns>
        private int GetCalulateSeconds(int duration) {
            if (duration <= 0) return 0;
            return duration * 3600;
        }

        /// <summary>
        /// Open worklist item by serial number
        /// </summary>
        /// <param name="sn">The worklist item's serial number</param>
        /// <returns>The worklist item</returns>
        public SourceCode.Workflow.Client.WorklistItem OpenWorklistItemBySN(string sn)
        {
            if (string.IsNullOrEmpty(sn)) return null;
            Connection conn = GetWorkflowClient();
            return OpenWorklistItem(conn, sn, _userAccount, "");
        }

        /// <summary>
        /// Start specific process instance
        /// </summary>
        /// <param name="instance">The process instance</param>
        public void CreateStartProcessInstance(K2.ProcessInstance instance)
        {
            if (instance == null)
                throw new ArgumentException("Process Instance parameter is null object.");
            using (Connection conn = GetWorkflowClient())
            {
                conn.StartProcessInstance(instance);
            }
        }

        /// <summary>
        /// Start specific process instance
        /// Return [ProcInstId]
        /// </summary>
        /// <param name="instance">The process instance</param>
        public int StartProcessInstance(InstParam instance)
        {
            int procInstId = 0;
            if (instance == null)
                throw new ArgumentException("Process Instance parameter is null object.");
            using (Connection conn = GetWorkflowClient())
            {
                var procInst = CreateInstance(instance.ProcName, instance.Folio, instance.DataFields, instance.Priority);
                conn.StartProcessInstance(procInst);
                procInstId = procInst.ID;
            }
            return procInstId;
        }


        /// <summary>
        /// Redirect worklist item to specific user by serial number
        /// </summary>
        /// <param name="serialNumber">The worklist item's serial number</param>
        /// <param name="destination">The destination user</param>
        public void Redirect(string serialNumber, string destination) {
            try {                
                using (Connection workflowClient = this.GetWorkflowClient()) {
                    K2.WorklistItem worklistItem = workflowClient.OpenWorklistItem(serialNumber, null, false);
                    if (worklistItem == null) {
                        throw new System.InvalidOperationException(string.Format("Not Found", serialNumber));
                    }
                    worklistItem.Redirect(destination);
                }
            } catch (UnauthorizedAccessException ex) {
                throw ex;
            }
        }


        /// <summary>
        /// Share worlist item to some users by serial number
        /// </summary>
        /// <param name="serialNumber">The worklist item's serial number</param>
        /// <param name="destination">The destination users</param>
        public void Share(string serialNumber, string destination) {
            try {
                using (Connection workflowClient = this.GetWorkflowClient()) {
                    K2.WorklistItem worklistItem = workflowClient.OpenWorklistItem(serialNumber, null, false);
                    if (worklistItem == null) {
                        throw new InvalidOperationException(string.Format("Not Found", serialNumber));
                    }
                    worklistItem.Delegate(new K2.Destination(destination));
                }
            } catch (UnauthorizedAccessException ex) {
                throw ex;
            }
        }


        /// <summary>
        /// Release worklist item by serial number
        /// </summary>
        /// <param name="serialNumber">The worklist item's serial number</param>
        public void Release(string serialNumber, string sharedUser, string managedUser) {
            try {
                using (var client = this.GetWorkflowClient()) {
                    var wl = client.OpenWorklistItem(serialNumber, "ASP", false, false, managedUser, sharedUser);
                    if (wl == null) {
                        
                        throw new InvalidOperationException(string.Format("Not Found", serialNumber));
                    }
                    wl.Release();
                }
            } catch (SmartException ex) {
                throw ex;
            }
        }


        /// <summary>
        /// Load worklist shared from K2 HostServer
        /// </summary>
        /// <returns>The worklist shares</returns>
        public WorklistShares LoadOOF() {
            WorklistShares result;
            using (Connection workflowClient = this.GetWorkflowClient()) {
                WorklistShares currentSharingSettings = workflowClient.GetCurrentSharingSettings(ShareType.OOF);
                result = currentSharingSettings;
            }
            return result;
        }


        /// <summary>
        /// Create or set the Out Of Office of current user
        /// </summary>
        /// <param name="wrapper">The Out Of Office criteria</param>
        /// <returns>Success(true) or Fail(False)</returns>
        public bool SetOutOfOffice(OOFWrapper wrapper) {

            if(wrapper.WorkType == null) {
                using (Connection workflowClient = this.GetWorkflowClient()) {
                    workflowClient.SetUserStatus(Convert.ToBoolean(wrapper.Status) ? UserStatuses.Available : UserStatuses.OOF);
                }

                return false;
            }

            using (Connection workflowClient = this.GetWorkflowClient()) {
                bool isNew = false;
                WorklistShares worklistShares = new WorklistShares();
                worklistShares = workflowClient.GetCurrentSharingSettings(ShareType.OOF);
                WorklistShare worklistShare = null;
                if(worklistShares.Count > 0) {
                    worklistShare = worklistShares[0];
                    worklistShare.ShareType = ShareType.OOF;
                } else {
                    isNew = true;
                }

                if(worklistShare == null) {
                    worklistShare = new WorklistShare();
                    worklistShare.ShareType = ShareType.OOF;
                    isNew = true;
                }
                
                
                worklistShare.StartDate = wrapper.StartDate;
                worklistShare.EndDate = wrapper.EndDate;

                WorkTypes workTypes = worklistShare.WorkTypes;
                WorkType workType = new WorkType();

                if (workTypes.Count > 0) {
                    workType = workTypes[0];
                } else {
                    workType = new WorkType();
                    workTypes.Add(workType);
                }
                workType.Name = Guid.NewGuid().ToString();
                Destinations destinations = new Destinations();

                foreach (DestinationDto dest in wrapper.WorkType.Destinations) {
                    if(SecurityLabelUtils.IsCorrectUserName(dest.LoginName)) {
                        var destination = new K2.Destination(SecurityLabelUtils.GetNameWithLabel(dest.LoginName), DestinationType.User);
                        destinations.Add(destination);
                    }                    
                }

                workType.Destinations = destinations;
                workType.WorkTypeExceptions = GetWorkTypeExceptions(wrapper.WorkType.WorkTypeExceptions);
                worklistShare.WorkTypes = workTypes;

                if(isNew) {
                    workflowClient.ShareWorkList(worklistShare);
                }
                workflowClient.UpdateWorkType(worklistShare.WorkTypes[0]);
                workflowClient.SetUserStatus(Convert.ToBoolean(wrapper.Status) ? UserStatuses.Available : UserStatuses.OOF);
            }
            
            return true;
        }


        /// <summary>
        /// Get The Out Of Office work type exception
        /// </summary>
        /// <param name="exceptionDtos">The Work type exceptions</param>
        /// <returns>The work type exception</returns>
        public WorkTypeExceptions GetWorkTypeExceptions(IList<WorkTypeExceptionDto> exceptionDtos) {

            WorkTypeExceptions worktypeExceptions = new WorkTypeExceptions();

            foreach (WorkTypeExceptionDto exceptionDto in exceptionDtos) {
                var exception = new WorkTypeException();
                Destinations destinations = new Destinations();
                foreach (DestinationDto dest in exceptionDto.Destinations) {
                    var destination = new K2.Destination(SecurityLabelUtils.GetNameWithLabel(dest.LoginName), DestinationType.User);
                    destinations.Add(destination);
                }
                exception.Name = exceptionDto.Name;
                exception.Destinations = destinations;
                exception.WorklistCriteria = this.configureWorklistCriteria(exceptionDto.ProcessPath, exceptionDto.Activity);
                worktypeExceptions.Add(exception);
            }

            return worktypeExceptions;
        }
        

        /// <summary>
        /// Edit or update the Out of Office forward users
        /// </summary>
        /// <param name="workType">The work type</param>
        /// <param name="oofSetting_workType">The Out of Office setting work type</param>
        /// <returns>The Forward users</returns>
        public Destinations UpdateOOFUser(WorkType workType, Dictionary<string, object> oofSetting_workType) {
            Destinations destinations = new Destinations();
            ArrayList arrayList = (ArrayList)oofSetting_workType["destinations"];
            foreach (K2.Destination destination in workType.Destinations) {
                foreach (Dictionary<string, object> dictionary in arrayList) {
                    DestinationType destinationType = (DestinationType)Enum.Parse(typeof(DestinationType), dictionary["destinationType"].ToString());
                    if (dictionary["id"] != null) {
                        string b = dictionary["id"].ToString();
                        if (destination.ID.ToString() == b) {
                            destination.DestinationType = destinationType;
                            destination.Name = dictionary["name"].ToString();
                            destinations.Add(destination);
                            break;
                        }
                    } else {
                        destinations.Add(new K2.Destination(dictionary["name"].ToString(), destinationType));
                    }
                }
            }
            return destinations;
        }

        /// <summary>
        /// Add or save new exception user for the Out of Office status
        /// </summary>
        /// <param name="oofException">The Out of Office exception users</param>
        /// <returns>The Exception users</returns>
        public Destinations AddExceptionUser(Dictionary<string, object> oofException) {
            Destinations destinations = new Destinations();
            ArrayList arrayList = (ArrayList)oofException["users"];
            for (int i = 0; i < arrayList.Count; i++) {
                Dictionary<string, object> dictionary = (Dictionary<string, object>)arrayList[i];
                string value = (string)dictionary["destinationType"];
                DestinationType type = (DestinationType)Enum.Parse(typeof(DestinationType), value);
                string name = (string)dictionary["name"];
                destinations.Add(new K2.Destination(name, type));
            }
            return destinations;
        }
        

        /// <summary>
        /// Configure worklist criteria for specific process name and activity name
        /// </summary>
        /// <param name="fullProcessName">The full path of workflow</param>
        /// <param name="activityName">The activity name of workflow</param>
        /// <returns>The worklist criteria</returns>
        public WorklistCriteria configureWorklistCriteria(string fullProcessName, string activityName) {
            WorklistCriteria worklistCriteria = new WorklistCriteria();
            WCField field = (WCField)Enum.Parse(typeof(WCField), "ProcessFullName");
            WCCompare compare = (WCCompare)Enum.Parse(typeof(WCCompare), "Equal");
            worklistCriteria.AddFilterField(field, compare, fullProcessName);
            WCField field2 = (WCField)Enum.Parse(typeof(WCField), "ActivityName");
            WCCompare compare2 = (WCCompare)Enum.Parse(typeof(WCCompare), "Equal");
            WCLogical logical = (WCLogical)Enum.Parse(typeof(WCLogical), "And");
            worklistCriteria.AddFilterField(logical, field2, compare2, activityName);
            return worklistCriteria;
        }


        /// <summary>
        /// Get In Of Office or Out Of Office of current user
        /// </summary>
        /// <returns>The Out Of Office(true) or In Of Office(false)</returns>
        public bool GetOOFStatus() {
            bool status = false;

            using (Connection workflowClient = this.GetWorkflowClient()) {
                switch (workflowClient.GetUserStatus()) {
                    case UserStatuses.None:
                    case UserStatuses.Available:
                        status = true;
                        break;
                    case UserStatuses.OOF:
                        status = false;
                        break;
                }
            }

            return status;
        }


        /// <summary>
        /// Get User Out of Office status from K2 HostServer
        /// </summary>
        /// <param name="originalDestinationUser">The Originator Destination User</param>
        /// <returns>The Worklist user status</returns>
        public WorklistUserStatus GetOOFStatus(string originalDestinationUser) {
            bool status = false;
            WorklistUserStatus worklistUserStatus = new WorklistUserStatus();
            using (Connection workflowClient = this.GetWorkflowClient()) {
                switch (string.IsNullOrEmpty(originalDestinationUser) ? workflowClient.GetUserStatus() : workflowClient.GetUserStatus(originalDestinationUser)) {
                    case UserStatuses.None:
                    case UserStatuses.Available:
                        status = true;
                        break;
                    case UserStatuses.OOF:
                        status = false;
                        break;
                }
                worklistUserStatus.Status = status;
                if (!worklistUserStatus.Status) {
                    WorklistShares worklistShares = new WorklistShares();
                    worklistShares = workflowClient.GetCurrentSharingSettings(ShareType.OOF);
                    if (worklistShares.Count > 0) {
                        WorklistShare worklistShare = worklistShares[0];
                        worklistShare.ShareType = ShareType.OOF;
                        List<OOFUser> list = new List<OOFUser>();
                        foreach (K2.Destination destination in worklistShare.WorkTypes[0].Destinations) {
                            list.Add(new OOFUser {
                                UserName = destination.Name,
                                Type = destination.DestinationType.ToString()
                            });
                        }
                        worklistUserStatus.users = list;
                    }
                }
            }
            return worklistUserStatus;
        }

        /// <summary>
        /// Get the worklist status of current user by serial number
        /// </summary>
        /// <param name="serial">The process serial number</param>
        /// <returns>The worklist item's status</returns>
        public K2.WorklistStatus GetWorklistStatus(string serial) {
            K2.WorklistStatus result = K2.WorklistStatus.Available;
            if (serial != null) {
                try {
                    WorklistItemCollection source = OpenWorklist();
                    Cores.WorklistItem worklistItem = (
                        from item in source
                        where item.SerialNumber == serial
                        select item).FirstOrDefault<Cores.WorklistItem>();
                    result = worklistItem.Status;
                } catch (UnauthorizedAccessException) {
                    throw;
                } catch (SmartException ex) {
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// Execute a worklist item's action by serial number
        /// </summary>
        /// <param name="serial">The serial number</param>
        /// <param name="actionName">The worklist item's action name</param>
        /// <param name="commonKey">The Common key data field</param>
        /// <param name="comment">The worklist item's comment</param>
        public void ExecuteWorklistItem(string serial, string actionName, IDictionary<string, object> dataFields) {
            if (dataFields == null) {
                dataFields = new Dictionary<string, object>();
            }
            using (Connection conn = GetWorkflowClient()) {
                string sharedUser = GetSharedUsersBySerialNumber(serial);
                string managedUser = GetFQN();
                SourceCode.Workflow.Client.WorklistItem item = conn.OpenWorklistItem(serial, "ASP", false, false, managedUser, sharedUser);
                if (item == null || item.Actions[actionName] == null) {
                    return;
                }
                
                string commentKey = string.Empty, comment = string.Empty;
                foreach (string key in _comments)
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
                    foreach (K2.DataField dataField in item.ProcessInstance.DataFields)
                    {
                        commentKey = _comments.SingleOrDefault(p => p == dataField.Name);
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
                    
                    keys.ForEach((key) => {
                        item.ProcessInstance.DataFields[key].Value = dataFields[key];
                    });
                }

                item.Actions[actionName].Execute();
            }


        }

        /// <summary>
        /// Create new Workflow instance
        /// </summary>
        /// <param name="fullProccessName">The full path of workflow process name</param>
        /// <param name="folio">The process instance folio</param>
        /// <param name="dataFields">The process instance data fields</param>
        /// <param name="priority">The process instance priority</param>
        /// <returns>The worklist item's process instance</returns>
        public SourceCode.Workflow.Client.ProcessInstance CreateInstance(string fullProccessName, string folio, IDictionary<string, object> dataFields, Priority priority) {
            if (string.IsNullOrEmpty(fullProccessName)) return null;
            if (string.IsNullOrEmpty(folio)) return null;

            using (Connection conn = GetWorkflowClient()) {
                SourceCode.Workflow.Client.ProcessInstance instance = conn.CreateProcessInstance(fullProccessName);
                instance.Folio = folio;
                instance.Priority = Convert.ToInt16(priority);

                string commentKey = null;

                foreach (K2.DataField dataField in instance.DataFields)
                {
                    commentKey = _comments.SingleOrDefault(p => p == dataField.Name);
                    if (commentKey != null) {
                        break;
                    }
                }
                
                if (dataFields != null && dataFields.Count > 0) {
                    List<string> keys = dataFields.Keys.ToList();
                    keys.ForEach((key) => {
                        if (commentKey != null && _comments.Contains(key))
                        {
                            instance.DataFields[commentKey].Value = dataFields[key];
                        }
                        else {
                            instance.DataFields[key].Value = dataFields[key];
                        }
                    });
                }
                return instance;
            }
        }

        /// <summary>
        /// Create new worklist item process instance
        /// </summary>
        /// <param name="fullProccessName">The full workflow process name</param>
        /// <param name="folio">The worklist item's folio</param>
        /// <param name="dataFields">The process instance data fields</param>
        /// <param name="priority">The worklist item's priority</param>
        /// <param name="xmlFields">The process instance's xml fields</param>
        /// <returns>The worklist item's process instance</returns>
        public SourceCode.Workflow.Client.ProcessInstance CreateInstance(string fullProccessName, string folio, IDictionary<string, object> dataFields, Priority priority, IDictionary<string, string> xmlFields) {
            if (xmlFields == null || xmlFields.Count == 0) return null;

            SourceCode.Workflow.Client.ProcessInstance instance = CreateInstance(fullProccessName, folio, dataFields, priority);

            if (instance == null) return null;

            List<string> keys = xmlFields.Keys.ToList();
            keys.ForEach((key) => {
                instance.XmlFields[key].Value = xmlFields[key];
            });
            return instance;
        }

        public K2.ProcessInstance GetProcessInstance(int instId)
        {
            using (Connection workflowClient = this.GetWorkflowClient())
            {
                return workflowClient.OpenProcessInstance(instId);
            }
        }

        public K2.WorklistItem OpenWorklistItem(string serialNumber, string sharedUser, string managedUser)
        {

            if (string.IsNullOrEmpty(serialNumber)) return null;

            if(sharedUser == null)
            {
                return OpenWorklistItemBySN(serialNumber);
            }

            Connection conn = null;
            try
            {
                conn = GetWorkflowClient();
                return OpenWorklistItem(conn, serialNumber, sharedUser, managedUser);
            }
            finally
            {
                conn.Close();
            }
            
        }

        public Domain.K2.WorklistItem OpenFormBySerial(string serialNumber)
        {   
            return ConvertTaskObject(GetWorklistItem(serialNumber));
        }

        public K2.WorklistItem GetWorklistItem(string serialNumber)
        {
            if (string.IsNullOrEmpty(serialNumber))
            {
                return null;
            }

            try
            {
                return OpenWorklistItemBySN(serialNumber);
            }
            catch (Exception ex)
            {
                using (var conn = GetWorkflowClient())
                {
                    var wl = _server.GetWorklistItem(serialNumber);

                    if (!string.IsNullOrEmpty(wl.Destination))
                    {
                        string sharedUser = wl.Destination;
                        string managedUser = _userAccount;
                        return OpenWorklistItem(conn, serialNumber, sharedUser, managedUser);
                    }
                }
            }
            return null;
        }

        private Domain.K2.WorklistItem ConvertTaskObject(K2.WorklistItem wli) {
            var actions = new List<string>();
            for (int i = 0; i < wli.Actions.Count; i++)
            {
                actions.Add(wli.Actions[i].Name);
            }

            return new Domain.K2.WorklistItem()
            {
                ActInstDestName = wli.ActivityInstanceDestination.Name,
                Actions = actions,
                SerialNumber = wli.SerialNumber,
                AllocatedUser = wli.AllocatedUser
            };
        }

        #endregion
    }
}
