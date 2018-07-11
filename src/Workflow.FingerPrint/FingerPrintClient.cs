using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using Workflow.DataAcess;
using Workflow.DataContract.Fingerprint;
using zkemkeeper;

namespace Workflow.FingerPrint {

    public class FingerPrintClient : IFingerPrintClient {

        private const int REG_NUMBER = 65535;
        public bool IsConnected = false;

        private string ip;
        private int port;
        private int machineNumber;

        public const string STATUS_CONNECTED = "CONNECTED";
        public const string STATUS_CONNECTING = "CONNECTING";
        public const string STATUS_DISCONNECT = "DIS_CONNECT";
        public const string STATUS_ERROR = "ERROR";
        public const string STATUS_RETRY = "RETRYING";

        public string IP { get { return ip; } set { ip = value; } }
        public int Port { get { return port; } set { port = value; } }
        public int MachineNumber { get { return machineNumber; } set { machineNumber = value; } }

        protected ConnectionConfig ConnectionConfig;

        public string Status { get; set; }

        protected Timer RetryTimer = null;
        protected int MaxTime = 5760;
        protected int RetryCount = 0;

        public CZKEMClass iClockClient = null;

        protected ILog logger = LogManager.GetLogger(typeof(FingerPrintClient));

        public event OnTouchHandler OnTouch;

        public FingerPrintClient(ConnectionConfig connectionConfig) {
            this.ConnectionConfig = connectionConfig;
            this.IP = ConnectionConfig.IP;
            this.Port = connectionConfig.Port;
            this.MachineNumber = connectionConfig.MachineNumber;
        }

        public void Connect(bool force, Func<CZKEMClass, bool, bool> onConnected) {
            try {
                if (string.IsNullOrEmpty(ConnectionConfig.IP) || ConnectionConfig.Port == 0) {
                    throw new Exception("IP and Port cannot be null or empty");
                }
                logger = LogManager.GetLogger(ToString());
                iClockClient = iClockClient ?? new CZKEMClass();
                if (IsConnected == true && force) {
                    iClockClient.Disconnect();
                    iClockClient.OnConnected += IClockClient_OnConnected;
                    iClockClient.OnDisConnected += IClockClient_OnDisConnected;
                    IsConnected = iClockClient.Connect_Net(ConnectionConfig.IP, ConnectionConfig.Port);
                }

                if (IsConnected == false) {
                    iClockClient.OnConnected += IClockClient_OnConnected;
                    iClockClient.OnDisConnected += IClockClient_OnDisConnected;
                    IsConnected = iClockClient.Connect_Net(ConnectionConfig.IP, ConnectionConfig.Port);
                }

                if (IsConnected == true) {
                    RegisterEvents();
                    Status = STATUS_CONNECTED;
                } else {
                    int idwErrorCode = 0;
                    iClockClient.GetLastError(ref idwErrorCode);
                    throw new Exception(idwErrorCode.ToString());
                }
                onConnected(iClockClient, IsConnected);
            } catch(Exception ex) {
                logger.Error(ex.Message, ex);
            }            
        }

        public void RetryConnect() {
            Status = STATUS_RETRY;
            UpdateFingerPrintStatus(STATUS_RETRY);
            RetryTimer = new Timer();
            RetryTimer.Interval = 5000;
            RetryTimer.Elapsed += RetryTimer_Elapsed;
            RetryTimer.Start();
            IsConnected = false;
        }

        private void RetryTimer_Elapsed(object sender, ElapsedEventArgs e) {
            try {
                RetryTimer.Stop();
                if (Ping3Times() && RetryCount < MaxTime) {                    
                    IsConnected = iClockClient.Connect_Net(ConnectionConfig.IP, Convert.ToInt32(ConnectionConfig.Port));
                    if (IsConnected) {
                        UnregisterEvents();
                        RegisterEvents();
                        Status = STATUS_CONNECTED;
                        UpdateFingerPrintStatus(STATUS_CONNECTED);
                        RetryTimer.Dispose();
                        RetryTimer = null;
                        RetryCount = 0;
                    }
                } else {
                    RetryCount++;
                    RetryTimer.Start();
                }
            } catch(Exception ex) {
                logger.Error(ex.Message, ex);
                RetryCount++;
                RetryTimer.Start();
            }
        }

        private void RegisterEvents() {
            if (iClockClient.RegEvent(ConnectionConfig.MachineNumber, REG_NUMBER)) {
                iClockClient.OnFinger += new _IZKEMEvents_OnFingerEventHandler(iClockClient_OnFinger);
                iClockClient.OnVerify += new _IZKEMEvents_OnVerifyEventHandler(iClockClient_OnVerify);
                iClockClient.OnAttTransactionEx += new _IZKEMEvents_OnAttTransactionExEventHandler(iClockClient_OnAttTransactionEx);
                iClockClient.OnFingerFeature += new _IZKEMEvents_OnFingerFeatureEventHandler(iClockClient_OnFingerFeature);
                iClockClient.OnEnrollFingerEx += new _IZKEMEvents_OnEnrollFingerExEventHandler(iClockClient_OnEnrollFingerEx);
                iClockClient.OnDeleteTemplate += new _IZKEMEvents_OnDeleteTemplateEventHandler(iClockClient_OnDeleteTemplate);
                iClockClient.OnNewUser += new _IZKEMEvents_OnNewUserEventHandler(iClockClient_OnNewUser);
                iClockClient.OnHIDNum += new _IZKEMEvents_OnHIDNumEventHandler(iClockClient_OnHIDNum);
                iClockClient.OnAlarm += new _IZKEMEvents_OnAlarmEventHandler(iClockClient_OnAlarm);
                iClockClient.OnDoor += new _IZKEMEvents_OnDoorEventHandler(iClockClient_OnDoor);
                iClockClient.OnWriteCard += new _IZKEMEvents_OnWriteCardEventHandler(iClockClient_OnWriteCard);
                iClockClient.OnEmptyCard += new _IZKEMEvents_OnEmptyCardEventHandler(iClockClient_OnEmptyCard);
            }
        }

        private void UnregisterEvents() {
            if (iClockClient.RegEvent(ConnectionConfig.MachineNumber, REG_NUMBER)) {
                iClockClient.OnFinger -= new _IZKEMEvents_OnFingerEventHandler(iClockClient_OnFinger);
                iClockClient.OnVerify -= new _IZKEMEvents_OnVerifyEventHandler(iClockClient_OnVerify);
                iClockClient.OnAttTransactionEx -= new _IZKEMEvents_OnAttTransactionExEventHandler(iClockClient_OnAttTransactionEx);
                iClockClient.OnFingerFeature -= new _IZKEMEvents_OnFingerFeatureEventHandler(iClockClient_OnFingerFeature);
                iClockClient.OnEnrollFingerEx -= new _IZKEMEvents_OnEnrollFingerExEventHandler(iClockClient_OnEnrollFingerEx);
                iClockClient.OnDeleteTemplate -= new _IZKEMEvents_OnDeleteTemplateEventHandler(iClockClient_OnDeleteTemplate);
                iClockClient.OnNewUser -= new _IZKEMEvents_OnNewUserEventHandler(iClockClient_OnNewUser);
                iClockClient.OnHIDNum -= new _IZKEMEvents_OnHIDNumEventHandler(iClockClient_OnHIDNum);
                iClockClient.OnAlarm -= new _IZKEMEvents_OnAlarmEventHandler(iClockClient_OnAlarm);
                iClockClient.OnDoor -= new _IZKEMEvents_OnDoorEventHandler(iClockClient_OnDoor);
                iClockClient.OnWriteCard -= new _IZKEMEvents_OnWriteCardEventHandler(iClockClient_OnWriteCard);
                iClockClient.OnEmptyCard -= new _IZKEMEvents_OnEmptyCardEventHandler(iClockClient_OnEmptyCard);
            }
        }

        public void Reconnect(Func<CZKEMClass, bool, bool> onCompleted) {
            if (iClockClient != null) {
                IsConnected = iClockClient.Connect_Net(ConnectionConfig.IP, ConnectionConfig.Port);
            }
            onCompleted(iClockClient, IsConnected);
        }

        private void IClockClient_OnDisConnected() {
            logger.Info("IClockClient_OnDisConnected");
            IsConnected = false;
        }

        private void IClockClient_OnConnected() {
            logger.Info("IClockClient_OnConnected");
            IsConnected = true;
            using (var dbContext = new WorkflowContext()) {
                var fingerPrintMachine = dbContext.FingerPrintMachines.FirstOrDefault(p => p.IP == IP);
                if (fingerPrintMachine != null) {
                    fingerPrintMachine.ConnectedDate = DateTime.Now;
                    fingerPrintMachine.Status = STATUS_CONNECTED;
                    dbContext.FingerPrintMachines.Attach(fingerPrintMachine);
                    dbContext.Entry(fingerPrintMachine).State = EntityState.Modified;
                    if (dbContext.ChangeTracker.HasChanges()) {
                        dbContext.SaveChanges();
                    }
                }
            }
        }

        private void iClockClient_OnEmptyCard(int ActionResult) {
            logger.Info("iClockClient_OnEmptyCard");
            string data = string.Format("ActionResult:{0}", ActionResult);
            logger.Info(data);
        }

        private void iClockClient_OnWriteCard(int EnrollNumber, int ActionResult, int Length) {
            logger.Info("iClockClient_OnWriteCard");
            string data = string.Format("EnrollNumber:{0}, ActionResult:{1}, Length:{2}", EnrollNumber, ActionResult, Length);
            logger.Info(data);
        }

        private void iClockClient_OnDoor(int EventType) {
            logger.Info("iClockClient_OnDoor");
            string data = string.Format("EventType:{0}", EventType);
            logger.Info(data);
        }

        private void iClockClient_OnAlarm(int AlarmType, int EnrollNumber, int Verified) {
            logger.Info("iClockClient_OnAlarm");
            string data = string.Format("AlarmType:{0}, EnrollNumber:{1}, Verified:{2}", AlarmType, EnrollNumber, Verified);
            logger.Info(data);
        }

        private void iClockClient_OnHIDNum(int CardNumber) {
            logger.Info("iClockClient_OnHIDNum");
            string data = string.Format("CardNumber:{0}", CardNumber);
            logger.Info(data);
        }

        private void iClockClient_OnNewUser(int EnrollNumber) {
            logger.Info("iClockClient_OnNewUser");
            string data = string.Format("EnrollNumber:{0}", EnrollNumber);
            logger.Info(data);
        }

        private void iClockClient_OnDeleteTemplate(int EnrollNumber, int FingerIndex) {
            logger.Info("iClockClient_OnDeleteTemplate");
            string data = string.Format("EnrollNumber:{0}, FingerIndex:{1}", EnrollNumber, FingerIndex);
            logger.Info(data);
        }

        private void iClockClient_OnEnrollFingerEx(string EnrollNumber, int FingerIndex, int ActionResult, int TemplateLength) {
            logger.Info("iClockClient_OnEnrollFingerEx");
            string data = string.Format("EnrollNumber:{0}, FingerIndex:{1}, ActionResult:{2}, TemplateLength:{3}", EnrollNumber, FingerIndex, ActionResult, TemplateLength);
            logger.Info(data);
        }

        private void iClockClient_OnFingerFeature(int Score) {
            logger.Info("iClockClient_OnFingerFeature");
            string data = string.Format("Score:{0}", Score);
            logger.Info(data);
        }

        private void iClockClient_OnAttTransactionEx(string EnrollNumber, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second, int WorkCode) {            
            logger.Info("iClockClient_OnAttTransactionEx");
            string data = string.Format("EnrollNumber:{0}, IsInValid:{1}, AttState:{2}, VerifyMethod:{3}, Year:{4}, Month:{5}, Day:{6}, Hour:{7}, Minute:{8}, Second:{9}, WorkCode:{10}", EnrollNumber, IsInValid, AttState, VerifyMethod, Year, Month, Day, Hour, Minute, Second, WorkCode);
            logger.Info(data);
            var evt = new iClockEventArg() {
                IP = this.IP,
                Port = Convert.ToInt16(this.Port),
                EnrollNumber = EnrollNumber,
                IsInValid = IsInValid,
                AttState = AttState,
                VerifyMethod = VerifyMethod,
                CreatedDate = new DateTime(Year, Month, Day, Hour, Minute, Second),
                WorkCode = WorkCode,
                MachineNo = this.MachineNumber
            };
            OnTouch(this, evt);
        }

        private void iClockClient_OnVerify(int UserID) {
            logger.Info("iClockClient_OnVerify");
            string data = string.Format("UserID:{0}", UserID);
            logger.Info(data);
        }

        private void iClockClient_OnFinger() {
            logger.Info("iClockClient_OnFinger");
        }

        public override string ToString() {
            return string.Format("IP:{0}, Port:{1}, MachineNo:{2}", ConnectionConfig.IP, ConnectionConfig.Port, ConnectionConfig.MachineNumber);
        }

        public void DisConnect() {
            DisConnect(STATUS_DISCONNECT);
        }

        public void DisConnect(string status) {
            if (IsConnected) {                
                iClockClient.Disconnect();
                IsConnected = false;
                UpdateFingerPrintStatus(status);
            }
        }

        public void UpdateFingerPrintStatus(string status) {
            using (var dbContext = new WorkflowContext()) {
                var fingerPrintMachine = dbContext.FingerPrintMachines.FirstOrDefault(p => p.IP == IP);
                if (fingerPrintMachine != null) {
                    fingerPrintMachine.LastConnectedDate = DateTime.Now;
                    fingerPrintMachine.Status = status;
                    dbContext.FingerPrintMachines.Attach(fingerPrintMachine);
                    dbContext.Entry(fingerPrintMachine).State = EntityState.Modified;
                    if (dbContext.ChangeTracker.HasChanges()) {
                        dbContext.SaveChanges();
                    }
                }
            }
        }

        public bool IsNetConnected() {
            bool pingable = false;
            Ping pinger = new Ping();
            try {
                PingReply reply = pinger.Send(IP);
                pingable = reply.Status == IPStatus.Success;
            } catch (PingException ex) {
                logger.Error(ex.Message, ex);
                pingable = false;
            }
            return pingable;
        }

        private bool Ping3Times() {
            bool isCon = true;
            for(int i = 0; i < 3; i++) {
                if (!IsNetConnected()) { isCon = false; break; }
            }
            return isCon;
        }
    }
}
