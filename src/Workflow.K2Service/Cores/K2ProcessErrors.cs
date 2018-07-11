using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using SourceCode.SmartObjects.Client;
using k2Base = SourceCode.Hosting.Client.BaseAPI;
using k2Client = SourceCode.Workflow.Client;
using k2Mgnt = SourceCode.Workflow.Management;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Workflow.K2Service.Cores {
    [Serializable]
    public class K2ProcessErrors : List<K2ProcessError>
    {
        #region private fields

        private string _filePath = null;
        private k2Base.SCConnectionStringBuilder _k2ConnectionStringBuilder = null;
        private string _errorProfileName;
        private bool _isDirty = false;
        private string connectionString = "";

        #endregion

        #region constructors

        /// <summary>
        /// Default parameterless constructor to facilitate serialization.
        /// </summary>
        public K2ProcessErrors()
            : base()
        {
            connectionString = ConfigurationManager.ConnectionStrings["HostServer"].ConnectionString;
            _errorProfileName = "All";
        }

        /// <summary>
        /// Preferred constructor.
        /// </summary>
        /// <param name="filePath"></param>
        public K2ProcessErrors(string filePath, k2Base.SCConnectionStringBuilder k2ConnectionStringBuilder, string errorProfileName)
            : base()
        {
            _filePath = filePath;
            _k2ConnectionStringBuilder = k2ConnectionStringBuilder;
            _errorProfileName = errorProfileName;
        }

        #endregion

        #region private methods

        #region Exists()...
        /// <summary>
        /// Method to indicate whether a collection item exists with the specified error ID.
        /// </summary>
        /// <param name="errorId"></param>
        /// <returns>A boolean indicating whether the item exists (true) or not (false).</returns>
        private bool Exists(int errorId)
        {
            K2ProcessError result = Find(errorId);
            return result != null;
        }
        #endregion

        #region Find()...
        /// <summary>
        /// Method to find and return a collection item with the specified error ID.
        /// </summary>
        /// <param name="errorId"></param>
        /// <returns>A K2ProcessError object</returns>
        private K2ProcessError Find(int errorId)
        {
            return (K2ProcessError)Find(
                delegate(K2ProcessError k2ErrorError)
                {
                    return k2ErrorError.ErrorId == errorId;
                });
        }
        #endregion

        #region RemoveAll()...
        /// <summary>
        /// Method to remove all errors from the collection with the specified process instance ID.
        /// </summary>
        /// <param name="procInstId"></param>
        private void RemoveAll(int procInstId)
        {
            int i = 0;
            while (i < Count)
            {
                if (this[i].ProcInstId == procInstId)
                {
                    RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
        #endregion

        #region ProcessInstanceExists
        /// <summary>
        /// Method to check if a specified process instance still exists.
        /// </summary>
        /// <param name="procInstID">The process instance ID that needs to be checked.</param>
        /// <param name="server">An instance of the workflow management server.</param>
        /// <returns></returns>
        private bool ProcessInstanceExists(int procInstID, k2Mgnt.WorkflowManagementServer server)
        {
            // use SmartObjectClientServer for accessing SmartObjects
            SmartObjectClientServer sos = new SmartObjectClientServer();

            // prepare the connection
            sos.CreateConnection();

            // open the connection
            sos.Connection.Open(connectionString);

            // get a "Process Instance" SmartObject (an ootB K2 SmartObject > Workflow Reports > Workflow General)
            SmartObject so = sos.GetSmartObject("Process_Instance");

            // tell the SmartObject to return a list of results
            so.MethodToExecute = "List";

            // set filter for results - return the live instance for the specified procInstID
            so.Properties["ProcessInstanceID"].Value = procInstID.ToString();

            // execute - return the results as a DataTable
            DataTable dt = sos.ExecuteListDataTable(so);

            // if there's one row the instance is still there and not deleted
            if (dt.Rows.Count == 1)
            {
                // close the connection and return true
                sos.Connection.Close();
                return true;
            }

            // if there is no row than the instance has been deleted
            return false;
        }

        #endregion

        #endregion

        #region public properties

        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        #region ErrorsToBeReported
        /// <summary>
        /// Method to return a collection of errors to be reported.
        /// </summary>
        /// <returns></returns>
        public K2ProcessErrors ErrorsToBeReported
        {
            get
            {
                K2ProcessErrors results = new K2ProcessErrors();
                foreach (K2ProcessError k2ProcessError in this)
                {
                    if (!k2ProcessError.Reported)
                    {
                        results.Add(k2ProcessError);
                    }
                }
                return results;
            }
        }

    
        #endregion

        #endregion

        #region public methods

        #region RetrieveNewErrors()...
        /// <summary>
        /// Method to check K2 for process errors since the last scheduled internal.
        /// </summary>
        public void RetrieveNewErrors()
        {
            k2Mgnt.WorkflowManagementServer server = new k2Mgnt.WorkflowManagementServer();
            server.Connection = server.CreateConnection();
            server.Connection.Open(connectionString);
            
            // client connection to K2 server.
            k2Client.Connection k2Conn = new k2Client.Connection();
            
            try
            {
                // Search for new process errors in the specified profile and update the error list.
                k2Mgnt.ErrorLogs els = server.GetErrorLogs(server.GetErrorProfile(_errorProfileName).ID);

                // Open client connection to K2 server.

                string host = Regex.Replace(connectionString, @"(.+)(Host\=)(.+)(;)(.+)", "$3");

                k2Conn.Open(host);

                // check every error in the error log
                foreach (k2Mgnt.ErrorLog el in els)
                {
                    Update(el, k2Conn);
                }

                if (ErrorsToBeReported.Count > 0)
                {
                    //
                    // Pull in the related tasks into the error collection.
                    //
                    k2Mgnt.WorklistItems worklistItems = 
                        server.GetWorklistItems(
                            new DateTime(1900, 1, 1),
                            DateTime.Now,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty);

                    foreach (SourceCode.Workflow.Management.WorklistItem worklistItem in worklistItems)
                    {
                        if (worklistItem.ProcessInstanceStatus == k2Mgnt.ProcessInstanceStatus.Error)
                        {
                            Update(worklistItem);
                        }
                    }
                }

                //Finally, prune the errors collection.
                Prune(k2Conn, server);
            }
            finally
            {
                if (server.Connection != null)
                {
                    server.Connection.Close();
                }

                if (k2Conn != null)
                {
                    k2Conn.Close();
                }
            }
        }
        #endregion

        #region Update(processInstance)...
        /// <summary>
        /// Method to update the error collection using the specified process instance object.
        /// </summary>
        /// <param name="processInstance"></param>
        public void Update(k2Mgnt.ErrorLog el, k2Client.Connection client)
        {
            // check if this error is logged / or if the same error ID if already logged
            if (!Exists(el.ID))
            {
                // get the process instance for the originator and startdate details
                k2Client.ProcessInstance procInst = client.OpenProcessInstance(el.ProcInstID);

                base.Add(
                    new K2ProcessError(
                        el.ID,
                        el.ProcInstID,
                        el.ProcessName,
                        el.Folio,
                        el.ErrorItemName,
                        el.Description,
                        el.ErrorDate,
                        procInst.StartDate,
                        procInst.Originator.FQN));

                _isDirty = true;
            }     
        }
        #endregion

        #region Update(worklistItem)...
        /// <summary>
        /// Method to update the error collection using the specified worklist item object.
        /// </summary>
        /// <param name="worklistItem"></param>
        public void Update(k2Mgnt.WorklistItem worklistItem)
        {
            foreach (K2ProcessError k2ProcessError in this)
            {
                if (k2ProcessError.ProcInstId == worklistItem.ProcInstID)
                {
                    // For efficiency - update the item's Process Name property as it may be blank.
                    k2ProcessError.ProcessName = worklistItem.ProcName;
                    
                    // Determine whether the task is already contained in the current item's tasks collection.
                    bool found = false;
                    foreach (K2Task task in k2ProcessError.Tasks)
                    {
                        if (task.EventID == worklistItem.EventID)
                        {
                            found = true;
                            break;
                        }
                    }

                    // If the task was not found, add it the current item's task collection.
                    if (!found)
                    {
                        k2ProcessError.Tasks.Add(new K2Task(worklistItem.EventID, worklistItem.EventName, worklistItem.StartDate));
                        _isDirty = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        #endregion
        
        #region Prune()...
        /// <summary>
        /// Method to prune collection by removing errors for process instances that are not in error state anymore.
        /// </summary>
        /// <param name="client">K2 Client connection for retreiving specific process instances.</param>
        public void Prune(k2Client.Connection client, k2Mgnt.WorkflowManagementServer server)
        {
            int i = 0;
            while (i < Count)
            {
                bool found = false;
                bool remove = false;
                k2Client.ProcessInstance procInst = null;

                // check if the process instance still exists and is not deleted
                if (ProcessInstanceExists(this[i].ProcInstId, server))
                {
                    // get the process instance that's logged
                    procInst = client.OpenProcessInstance(this[i].ProcInstId);
                    // process instance is still alive!
                    found = true;

                    // now determine whether or not the process instance is still in an error state.
                    // if it is no longer in an error state, flag that it can be removed from the error collection.
                    if (procInst.Status1 != k2Client.ProcessInstance.Status.Error)
                    {
                        remove = true;
                    }
                }
       
                // if the associated process instance is no longer in an error state OR it has since been 
                // 'fixed and completed' or simply deleted, remove the error item from the error collection.
                if ((!found) || remove)
                {
                    RemoveAt(i);
                    _isDirty = true;
                }
                else
                {
                    i++;
                }
            }            
        }
        #endregion
        
        #region MarkAsReported
        /// <summary>
        /// Method to mark all error items as reported.
        /// </summary>
        public void MarkAsReported()
        {
            foreach (K2ProcessError k2ProcessError in this)
            {
                k2ProcessError.Reported = true;
            }

            _isDirty = true;
        }
        /// <summary>
        /// Method to mark all errors for a specific process name as reported.
        /// </summary>
        /// <param name="processName">The name of the process.</param>
        public void MarkAsReported(string processName)
        {
            foreach (K2ProcessError k2ProcessError in this)
            {
                if (processName == k2ProcessError.ProcessName)
                {
                    k2ProcessError.Reported = true;
                }
            }

            _isDirty = true;
        }

        #endregion

        #region Load()...
        /// <summary>
        /// Method to return the K2 process errors from the file store.
        /// </summary>
        public void Load()
        {
            if (File.Exists(_filePath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(_filePath);
                K2ProcessErrors k2ProcessErrors = (K2ProcessErrors)Serializer.Deserialise(typeof(K2ProcessErrors), xmlDoc.InnerXml);

                foreach (K2ProcessError k2ProcessError in k2ProcessErrors)
                {
                    Add(k2ProcessError);
                }
            }
        }
        #endregion

        #region Save()...
        /// <summary>
        /// Method to save changes to the file store.
        /// </summary>
        public void Save()
        {
            _isDirty = false;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(Serializer.Serialize(this));
            xmlDoc.Save(_filePath);
        }
        #endregion

        #region GetXmlDocument()...
        /// <summary>
        /// Method return an XmlDocument object representation of this object.
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetXmlDocument()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(Serializer.Serialize(this));
            return xmlDoc;
        }
        #endregion

        #region GetErrorsToBeReportedForProcessName
        /// <summary>
        /// Method to return a collection of errors to be reported for a specific processName.
        /// </summary>
        /// <returns></returns>
        public K2ProcessErrors GetErrorsToBeReportedForProcessName(string processName)
        {

            K2ProcessErrors results = new K2ProcessErrors();
            foreach (K2ProcessError k2ProcessError in this)
            {
                if (!k2ProcessError.Reported)
                {
                    // only add the errors for the requested process name
                    if (k2ProcessError.ProcessName == processName)
                    {
                        results.Add(k2ProcessError);
                    }
                }
            }
            return results;

        }
        
        #endregion

        #endregion
    }
}
