using SourceCode.Security.UserRoleManager.Management;
using SourceCode.Workflow.Management.Criteria;
using SourceCode.Workflow.Management;
using System.Collections.Generic;
using System.Configuration;
using System;
using System.Data;
using System.Linq;
using Workflow.DataContract.K2;

namespace Workflow.K2Service
{
    public class K2Server: IK2Server, IDisposable {

        #region Data Memebers

        public string ManagedUser { get; set; }

        protected static WorkflowManagementServer _server = null;

        #endregion

        #region Contructors

        public K2Server()
        {
            ConnectionFactory();
        }

        public K2Server(string userName)
        {
            ManagedUser = userName;
            ConnectionFactory();
        }

        private void ConnectionFactory() {
            if (_server == null || !_server.Connection.IsConnected)
            {
                _server = new WorkflowManagementServer();
                try
                {
                    var setting = ConfigurationManager.ConnectionStrings["HostServer"];
                    _server.Open(setting.ConnectionString);
                }
                catch (SmartException e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }
            }
        }

        #endregion

        #region Methods

        public List<string> GetProcessListByUser(string loginName)
        {
            var workflows = new List<string>();
            Permissions permissions = _server.GetUserPermissions(loginName.FQNWithK2Label(), true);

            foreach (ProcSetPermissions permission in permissions)
            {
                if (permission.Start)
                {
                    workflows.Add(permission.ProcessFullName);
                }
            }

            return workflows;
        }

        public bool CanStartWorkFlow(string loginName, string procFullPath) {

            Permissions permissions = _server.GetUserPermissions(loginName.FQNWithK2Label(), true);

            foreach (ProcSetPermissions permission in permissions)
            {
                if (permission.Start && permission.ProcessFullName.Equals(procFullPath, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public WorklistItem GetWorklistItem(string serialNo, ProcStatus status = ProcStatus.Open)
        {
            var items = GetWorklistItems(serialNo);
            var wlItems = new List<WorklistItem>();
            
            foreach (WorklistItem item in items)
            {
                bool match = false;
                if (status == ProcStatus.Active)
                {
                    if (item.Status == GetStatus(ProcStatus.Allocated) ||
                            item.Status == GetStatus(ProcStatus.Open) ||
                            item.Status == GetStatus(ProcStatus.Available)
                        )
                    {
                        match = true;
                    }
                } else {
                    if (item.Status == GetStatus(status)) {
                        match = true;
                    }
                }
                
                if (match && item.ProcessInstanceStatus == ProcessInstanceStatus.Active)
                {
                    wlItems.Add(item);
                }
            }

            if (wlItems.Count > 1)
            {
                return wlItems.SingleOrDefault(p => p.Destination == ManagedUser);
            }
            else {
                return wlItems.SingleOrDefault();
            }
        }

        private WorklistItem.WorklistStatus GetStatus(ProcStatus status) {
            switch (status)
            {
                case ProcStatus.Available:
                    return WorklistItem.WorklistStatus.Available;
                case ProcStatus.Open:
                    return WorklistItem.WorklistStatus.Open;
                case ProcStatus.Allocated:
                    return WorklistItem.WorklistStatus.Allocated;
                case ProcStatus.Sleep:
                    return WorklistItem.WorklistStatus.Sleep;
                case ProcStatus.Completed:
                    return WorklistItem.WorklistStatus.Completed;
                default:
                    return WorklistItem.WorklistStatus.Open;
            }
        }

        private WorklistItems GetWorklistItems(string serialNo)
        {
            WorklistCriteriaFilter criteria = new WorklistCriteriaFilter();
            RegularFilter rfPID = new RegularFilter();
            RegularFilter rfActID = new RegularFilter();
            rfPID.ColumnName = "PI.ID";
            rfPID.ParameterValue = serialNo.Substring(0, serialNo.IndexOf('_'));
            rfPID.DbType = DbType.Int32;
            rfPID.Comparison = Comparison.Equals;
            rfPID.ParameterName = "@ICE_ProcInstID";
            criteria.FilterCollection.Add(rfPID);

            rfActID.Condition = RegularFilter.FilterCondition.AND;
            rfActID.ColumnName = "WLH.ActInstDestID";
            rfActID.ParameterValue = serialNo.Substring(serialNo.IndexOf('_') + 1);
            rfActID.Comparison = Comparison.Equals;
            rfActID.ParameterName = "@ICE_ActInstID";
            rfActID.DbType = DbType.Int32;
            criteria.FilterCollection.Add(rfActID);

            return _server.GetWorklistItems(criteria);
        }

        public WorklistItems GetWorklistItemsByProcInstId(int procInstId)
        {
            WorklistCriteriaFilter criteria = new WorklistCriteriaFilter();
            RegularFilter rfPID = new RegularFilter();
            RegularFilter rfActID = new RegularFilter();
            rfPID.ColumnName = "PI.ID";
            rfPID.ParameterValue = procInstId;
            rfPID.DbType = DbType.Int32;
            rfPID.Comparison = Comparison.Equals;
            rfPID.ParameterName = "@ICE_ProcInstID";
            criteria.FilterCollection.Add(rfPID);

            return _server.GetWorklistItems(criteria);
        }

        public WorklistItem GetWorklistItem(int procInstId, string destUser)
        {

            WorklistCriteriaFilter criteria = new WorklistCriteriaFilter();
            RegularFilter rfPID = new RegularFilter();
            RegularFilter rfDestUser = new RegularFilter();
            rfPID.ColumnName = "PI.ID";
            rfPID.ParameterValue = procInstId;
            rfPID.DbType = DbType.Int32;
            rfPID.Comparison = Comparison.Equals;
            rfPID.ParameterName = "@ICE_ProcInstID";
            criteria.FilterCollection.Add(rfPID);

            rfDestUser.ColumnName = "PI.Destination";
            rfDestUser.ParameterValue = destUser;
            rfDestUser.DbType = DbType.String;
            rfDestUser.Comparison = Comparison.Equals;
            rfDestUser.ParameterName = "@ICE_Destination";
            criteria.FilterCollection.Add(rfDestUser);

            var items = _server.GetWorklistItems(criteria);

            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if ((items[i].Status == WorklistItem.WorklistStatus.Allocated ||
                        items[i].Status == WorklistItem.WorklistStatus.Open)
                        && items[i].ProcessInstanceStatus == ProcessInstanceStatus.Active)
                    {
                        return items[i];
                    }
                }
            }
            return null;
        }

        public bool ReleaseWorklistItem(string serialNo)
        {
            WorklistItem wi = GetWorklistItem(serialNo);
            if (wi != null)
            {
                _server.ReleaseWorklistItem(wi.ID);
                return true;
            }
            return false;
        }

        public ErrorLogs GetWorkflowErrors() {
            ErrorLogCriteriaFilter filter = new ErrorLogCriteriaFilter();
            filter.AddRegularFilter(ErrorLogFields.Date, Comparison.GreaterOrEquals, DateTime.Now.Date);
            ErrorLogs errorLogs = _server.GetErrorLogs(_server.GetErrorProfile("All").ID, filter);
            return errorLogs;
        }

        public bool RetryError(int procInstID, int procId = 0)
        {
            //string k2User = @"K2:" + ConfigurationManager.AppSettings["K2:AdminUser"];
            //var filter = new ErrorLogCriteriaFilter();
            //filter.AddRegularFilter(ErrorLogFields.ProcInstID, Comparison.Equals, procInstID);

            //var errors = GetWorkflowErrors();
            //bool result = false;
            //for (int index = 0; index < errors.Count; index++) {
            //    if (procId > 0)
            //    {
            //        result = RetryError(procInstID, errors[index].ID, procId, k2User);
            //    } else {
            //        result = RetryError(procInstID, errors[index].ID, k2User);
            //    }
            //}

            return true;
        }

        public void Dispose()
        {
            _server.Connection.Close();
        }
        #endregion
    }
}
