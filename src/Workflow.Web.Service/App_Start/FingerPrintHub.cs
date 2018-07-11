using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Workflow.DataAcess.Repositories;
using Workflow.DataContract.Fingerprint;
using Workflow.DataObject.MTF;
using Workflow.RabbitMQ;
using Workflow.Web.Service.Models;

namespace Workflow.Web.Service
{

    public class FingerPrintHub : Hub
    {
        private static int _preTotalCount = 0;
        //private static QueueTCPClient _client = null;
        private static Timer timer = null;
        private readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static HashSet<string> ConnectedIds = new HashSet<string>();
        private static RabbitMQClient _client = null;


        public override Task OnConnected()
        {
            ConnectedIds.Add(Context.ConnectionId);
            logger.Info("FingerPrintHub - Connected to: " + Context.Request.GetHttpContext().Request.ServerVariables["REMOTE_ADDR"]);
            logger.Info("FingerPrintHub - Total Connection: " + ConnectedIds.Count);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled) {
            logger.Info("FingerPrintHub - Disconnecting from: " + Context.Request.GetHttpContext().Request.ServerVariables["REMOTE_ADDR"]);
            ConnectedIds.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
        
        public FingerPrintHub()
        {
            if (_client == null) {
                _client = new RabbitMQClient("MT Dashboard Queue");
                _client.OnRecieved += (model, message) =>
                {
                    var cmdObject = _client.JsonDeserialize(message);
                    switch (cmdObject.Command)
                    {
                        case MessageCommandEnum.PUSH:
                            {
                                var toasts = ExecutePatientProcedure(Convert.ToInt32(cmdObject.Data), "FINGER_PRINT");
                                Clients.All.ReloadPatientList(toasts);
                                break;
                            }
                    }
                };
            }
            RunTask();
        }

        public void RunTask()
        {
            if (timer == null) {
                timer = new Timer();
                timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                timer.Interval = 10000;
                timer.Enabled = true;
                timer.Start();
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (HasUpdate())
            {
                var toasts = new List<object>();
                toasts.Add(new {
                    STATE = "SUBMIT_APPROVED_DONE"
                });
                Clients.All.ReloadPatientList(toasts);
            }
        }
        
        public void ProcessPatient(int id, string state) {
            var toasts = ExecutePatientProcedure(id, state);            
            Clients.All.ReloadPatientList(toasts);
        }

        public void Client_OnRecieved(System.Net.Sockets.Socket socket, string commandJson)
        {
            var cmdObject = JsonConvert.DeserializeObject<CommandObject>(commandJson);
            switch (cmdObject.Command)
            {
                case MessageCommandEnum.PUSH:
                {
                    var toasts = ExecutePatientProcedure(Convert.ToInt32(cmdObject.Data), "FINGER_PRINT");
                    Clients.All.ReloadPatientList(toasts);
                    break;
                }
            }
        }

        #region Private Method

        private bool HasUpdate()
        {
            Repository<PatientQueue> _services = new Repository<PatientQueue>();
            string sql = @"
                SELECT COUNT(1) TOTAL_COUNT FROM BPMDATA.APPROVAL_COMMENT C 
                INNER JOIN BPMDATA.REQUEST_HEADER H ON H.ID = C.REQUEST_HEADER_ID
                WHERE REQUEST_CODE = 'MT_REQ' AND C.APPROVAL_DATE > DATEADD(HOUR, -8, GETDATE())";

            int _counts = _services.Single(sql);
            if (_counts != 0) {
                int curTotalCount = Convert.ToInt32(_counts);
                if (curTotalCount != _preTotalCount)
                {
                    _preTotalCount = curTotalCount;
                    return true;
                }
            }
            
            return false;
        }

        private object ExecutePatientProcedure(int id, string state)
        {
            Repository<PatientQueue> _services = new Repository<PatientQueue>();
            return _services.SqlQuery<MsgView>(string.Format(
                        "EXEC [QUEUE].[SP_PATIENT] @ID = {0}, @STATE = '{1}'",
                        id, state)
                        );
        }

        private object GetPatientList()
        {
            Repository<PatientQueue> _services = new Repository<PatientQueue>();
            return _services.SqlQuery<CheckInView>(
                        string.Format(
                        " SELECT * FROM [QUEUE].[V_PATIENT] " +
                        " ORDER BY[PRIORITY], [LAST_MODIFIED_DATE] DESC ")
                        );
        } 
        #endregion
    }
}