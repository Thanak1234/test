using log4net;
using Microsoft.Exchange.WebServices.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Workflow.DataAcess;
using Workflow.DataContract;
using Workflow.DataContract.Fingerprint;
using Workflow.Domain.Entities.Queue;
using Workflow.MSExchange;
using Workflow.MSExchange.Core;
using Workflow.RabbitMQ;

namespace Workflow.FingerPrint
{
    public class FingerPrintManager : IFingerPrintManager {

        protected List<IFingerPrintClient> fingerPrintClients;
        protected ILog logger = LogManager.GetLogger(typeof(FingerPrintManager));
        protected List<Socket> socketClients = null;
        protected RabbitMQClient client;

        public FingerPrintManager() {
            fingerPrintClients = new List<IFingerPrintClient>();
            client = new RabbitMQClient("Fingerpring Manager Queue");
        }

        public FingerPrintManager(List<Socket> socketClients) {
            fingerPrintClients = new List<IFingerPrintClient>();
            this.socketClients = socketClients;
        }

        public void Startup() {
            using (var dbContext = new WorkflowContext()) {
                try {
                    var fingerPrintMachines = dbContext.FingerPrintMachines.Where(p => p.Active).ToList();
                    if (fingerPrintMachines.Count > 0) {
                        fingerPrintMachines.ForEach(entity => {
                            ConnectionConfig config = new ConnectionConfig() {
                                IP = entity.IP,
                                Port = entity.Port,
                                MachineNumber = entity.MachineNo,
                                Status = entity.Status
                            };
                            IFingerPrintClient client = new FingerPrintClient(config);
                            client.OnTouch += FingerprintClient_OnTouch;
                            fingerPrintClients.Add(client);
                        });
                    }
                } catch (Exception ex) {
                    logger.Error(ex.Message, ex);
                }
            }
        }

        private void FingerprintClient_OnTouch(FingerPrintClient client, iClockEventArg evt) {
            try {
                using (var context = new WorkflowContext()) {
                    var entity = new Domain.Entities.Queue.FingerPrint();
                    entity.AttState = evt.AttState;
                    entity.EnrolmentNo = evt.EnrollNumber;
                    entity.IP = evt.IP;
                    entity.MachineNo = evt.MachineNo;
                    entity.IsInvalid = evt.IsInValid;
                    entity.MachineDate = evt.CreatedDate;
                    entity.Port = evt.Port;
                    entity.Status = "NOT_QUEUE";
                    entity.VerifyMethod = evt.VerifyMethod;
                    entity.WorkCode = evt.WorkCode;
                    context.FingerPrints.Add(entity);
                    if (context.ChangeTracker.HasChanges()) {
                        context.SaveChanges();
                    }
                    SendToRabbitMQ(MessageCommandEnum.PUSH, entity.Id.ToString());
                }
            } catch (Exception ex) {
                logger.Error(ex.Message, ex);
            }
        }

        public void Start() {
            fingerPrintClients.ForEach(c => {
                try {
                    c.Connect(true, (iclock, connected) => {
                        if (connected) {
                            FingerPrintClient client = c as FingerPrintClient;
                            logger.InfoFormat("{0} connected", c.ToString());
                            SendToRabbitMQ(MessageCommandEnum.CONNECT, client.IP);
                        }
                        return true;
                    });
                } catch (Exception ex) {
                    logger.Error(ex.Message, ex);
                }
            });
        }

        private void SoketNetworkSentClient(MessageCommandEnum command, object data) {
            try {
                logger.Info("send to client");
                string json = GetJsonString(command, data);
                if (socketClients != null && socketClients.Count > 0) {
                    socketClients.ForEach(s => {
                        s.Send(Encoding.ASCII.GetBytes(json));
                        logger.InfoFormat("Data: {0}", json);
                    });
                }
                logger.Info("sent to client");
            } catch (Exception ex) {
                logger.Error(ex.Message, ex);
            }
        }

        protected void SendToRabbitMQ(MessageCommandEnum command, string message)
        {
            try
            {
                logger.Info("Send to RabbitMQ Server");
                client.Publish(command, message);
                logger.Info("Send to RabbitMQ Server");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
        }

        public void Stop() {
            fingerPrintClients.ForEach(c => {
                try {
                    c.DisConnect();
                } catch (Exception ex) {
                    logger.Error(ex.Message, ex);
                }
            });

            if(client != null)
            {
                client.CloseConnection();
            }
        }

        private string GetJsonString(MessageCommandEnum command, object data) {
            CommandObject obj = new CommandObject() {
                Command = command,
                Data = data
            };
            var setting = new JsonSerializerSettings();
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return JsonConvert.SerializeObject(obj, setting);
        }

        public void Disconnect(string ip) {
            if (fingerPrintClients != null && fingerPrintClients.Count > 0) {
                fingerPrintClients.ForEach(client => {
                    if (client.IP.Equals(ip, StringComparison.InvariantCultureIgnoreCase)) {
                        client.DisConnect();
                    }
                });
            }
        }

        public void Connect(string ip) {
            if (fingerPrintClients != null && fingerPrintClients.Count > 0) {
                var client = fingerPrintClients.FirstOrDefault(c => c.IP == ip);
                if (client != null) {
                    client.Connect(false, (iclock, connected) => {
                        if (connected) {
                            logger.InfoFormat("{0} connected", client.ToString());
                            SendToRabbitMQ(MessageCommandEnum.CONNECT, client.IP);
                        }
                        return true;
                    });
                }
            }
        }

        public void PingFingerPrintConnection() {
            try {
                if (fingerPrintClients != null && fingerPrintClients.Count > 0) {
                    fingerPrintClients.ForEach(c => {
                        FingerPrintClient client = c as FingerPrintClient;
                        bool isConnected = client.IsNetConnected();
                        if (!isConnected && client.IsConnected == true) {
                            logger.ErrorFormat("{0} not reply", client.ToString());
                            client.RetryConnect();
                            SendToRabbitMQ(MessageCommandEnum.CONNECTION_ERROR, client.IP);
                            SendError(client);
                        }
                    });
                }
            } catch(Exception ex) {
                logger.Error(ex.Message, ex);
            }
        }

        private void SendError(FingerPrintClient client) {
            try {
                string email = ConfigurationManager.AppSettings["Email"] != null ? ConfigurationManager.AppSettings["Email"] : "k2admin@nagaworld.com";
                string password = ConfigurationManager.AppSettings["Password"] != null ? ConfigurationManager.AppSettings["Password"] : "**aa12345";
                string reciepent = ConfigurationManager.AppSettings["Reciepent"] != null ? ConfigurationManager.AppSettings["Reciepent"] : "imsopheap@nagaworld.com";
                List<string> reciepents = new List<string>();
                if (reciepent.Split(',').Count() > 1) {
                    var emails = reciepent.Split(',');
                    foreach (string item in emails) {
                        reciepents.Add(item);
                    }
                } else {
                    reciepents.Add(reciepent);
                }
                var userInfo = UserInfo.CreateUserData(email, password, ExchangeVersion.Exchange2013);
                var emailService = new EmailService(userInfo);
                string message = string.Format("{0} network connection was losted please try to ping the Fingerprint IP: {1} if it is still problem, please contact to Programming Teams. <br/><br/>Note: Fingerprint is going to reconnect if network connection work properly.", client, client.IP);
                emailService.SendEmail("iClock Queue Service Alert", GetEmailContent(message), reciepents);
            } catch (Exception e) {
                logger.Error(e.Message, e);
            }

        }

        private string GetEmailContent(string message) {
            return @"<html>
                        <header>
                            <style type=""text/css"">
                                table.gridtable {
                                    font-family: verdana,arial,sans-serif;
                                    font-size: 11px;
                                    color: #333333;
                                    border-width: 1px;
                                    border-color: #666666;
                                    border-collapse: collapse;
                                }

                                    table.gridtable th {
                                        border-width: 1px;
                                        padding: 8px;
                                        border-style: solid;
                                        border-color: #666666;
                                        background-color: #dedede;
                                    }

                                    table.gridtable td {
                                        border-width: 1px;
                                        padding: 8px;
                                        border-style: solid;
                                        border-color: #666666;
                                        background-color: #ffffff;
                                    }
                            </style>
                        </header>
                        <body>
                            Dear All,
                            <br />
                            <h5 style='color:#347D7E; font-weight: bold'>iCLOCK QUEUE SERVICE ALERT</h5>
                            <br />" + message + @"
                            <br /><br /><span style='font-size:12.0pt;color:#1F497D'>Regards,</span><br /><b><u><span style='font-size:12.0pt;color:#347D7E'>Group IT Department</span></u></b>
	                        <br/><br/><b>Internet E-mail Confidentiality Footer</b><br>This E-mail is confidential and intended only for the use of the individual(s) or entity named above and may contain information that is privileged. If you are not the intended recipient, you are advised that any dissemination, distribution or copying of this E-mail is strictly prohibited. If you have received this E-mail in error, please notify us immediately by return E-mail or telephone and destroy the original message.</p>
                        </body>
                        </html>";
        }
    }
}
