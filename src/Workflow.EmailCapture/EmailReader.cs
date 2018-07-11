using log4net;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Timers;
using System.Diagnostics;
using System.Configuration;

namespace Workflow.EmailCapture {

    using MSExchange;
    using MSExchange.Core;
    using DataAcess;
    using Domain.Entities.Email;

    public class EmailReader : IEmailReader, INotificationHandler {

        #region Properties and Contructors

        protected ILog _Logger = null;
        protected IEmailService _Emailservice = null;
        protected UserInfo _UserInfo = null;
        protected EventType[] _EventType = new EventType[] { EventType.NewMail };
        protected const string EMAIL_STATUS = "NEW";
        protected Timer timer = null;
        protected long TotalRun = 0;
        protected int Retry = 0;
        protected int TotalRetry = 3;

        public EmailReader(UserInfo userInfo) {
            _UserInfo = userInfo;
            _Logger = LogManager.GetLogger(string.Format("login user<{0}>", _UserInfo.EmailAddress));
            //_Emailservice = new EmailService(_UserInfo);
            if (ConfigurationManager.AppSettings["Retry"] != null) {
                TotalRetry = Int32.Parse(ConfigurationManager.AppSettings["Retry"]);
            }
        }

        private void ExchangeConnect() {
            _Logger.Info("start email connecting...");
            if(_Emailservice != null) {
                _Emailservice.Dispose();
                _Emailservice = null;
            }
            _Emailservice = new EmailService(_UserInfo);
            _Logger.Info("email connection started...");
        }

        #endregion

        #region Implementation methods

        /// <summary>
        /// Stop all streaming connection
        /// </summary>
        public void StopNotification() {
            if (_Emailservice != null) {
                _Emailservice.DestroyAllConnection();
            }
        }

        /// <summary>
        /// Stop timer for schedule
        /// </summary>
        public void StopSchedule() {
            if (timer != null) {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
        }

        /// <summary>
        /// Register the email user in the StreamingSubcribing service for get new email
        /// </summary>
        public void StartNotification() {
            _Emailservice.RegisterNotification(_EventType, this);
        }

        /// <summary>
        /// Start schedule by interval for pulling new email in inbox folder
        /// </summary>
        public void StartSchedule() {
            long interval = long.Parse(ConfigurationManager.AppSettings["ScheduleInterval"]);
            timer = new Timer();
            timer.Enabled = true;
            timer.Interval = interval == 0 ? 30000 : interval;
            timer.Elapsed += OnElapsedEventHandler;
            timer.Start();
        }

        #endregion

        #region Events
        /// <summary>
        /// The StreamingSubscriptionConnection's disconnected event
        /// </summary>
        /// <param name="sender">The email service connection</param>
        /// <param name="args">The email service args</param>
        public void OnNotificationDisconnect(object sender, SubscriptionErrorEventArgs args) {
            _Logger.Info("notification disconnected.");
            var conn = (StreamingSubscriptionConnection)sender;
            if (conn != null && conn.IsOpen == false) {
                conn.Open();
                _Logger.Info("notification reconnected.");
                PullEmail();
            }
        }

        /// <summary>
        /// The StreamingSubscriptionConnection got errors event
        /// </summary>
        /// <param name="sender">The email service connection</param>
        /// <param name="args">The email service args</param>
        public void OnNotificationError(object sender, SubscriptionErrorEventArgs args) {
            _Logger.Error(args.Exception.Message, args.Exception);
        }

        /// <summary>
        /// The StreamingSubscriptionConnection got new email event
        /// </summary>
        /// <param name="sender">The email service connection</param>
        /// <param name="args">The email service args</param>
        public void OnNotificationEvent(object sender, NotificationEventArgs args) {
            try {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                foreach (NotificationEvent notification in args.Events) {
                    if (notification is ItemEvent) {
                        ItemEvent itemEvent = (ItemEvent)notification;
                        _Logger.Debug("Mail Item Id: " + itemEvent.ItemId.UniqueId);
                        var item = _Emailservice.BindToEmailMessage(itemEvent.ItemId);

                        if (item == null)
                            _Logger.Debug(string.Format("Mail id ({0}) not found in server.", itemEvent.ItemId.UniqueId));

                        SaveEmail(item);
                    }
                }
                stopwatch.Stop();
                _Logger.Debug(string.Format("On Notification Event(duration = {0} milisec)", stopwatch.Elapsed));
            } catch (ServiceRequestException se) {
                _Logger.Error(se.Message, se);
                SendError(se);
                Reconnect();
            } catch (Exception ex) {
                _Logger.Error(ex.Message, ex);
                SendError(ex);
            }
        }

        /// <summary>
        /// The schedule event happened base on interval
        /// </summary>
        /// <param name="sender">The timer object</param>
        /// <param name="e">The timer args</param>
        private void OnElapsedEventHandler(object sender, ElapsedEventArgs e) {
            Stopwatch stopwatch = new Stopwatch();
            try {

                timer.Stop();

                stopwatch.Start();                

                if (TotalRun == long.MaxValue)
                    TotalRun = 0;

                _Logger.Debug(string.Format("pull new email by schedule(Total running count = {0})", ++TotalRun));

                PullEmail();

                stopwatch.Stop();

                _Logger.Debug(string.Format("pull new email by schedule(duration = {0} milisec)", stopwatch.Elapsed));

                timer.Start();

            } catch (Exception ex) {
                _Logger.Error(ex.Message, ex);
                SendError(ex);
                stopwatch.Stop();
                timer.Start();
            }
        }

        #endregion

        #region Helper methods
        /// <summary>
        /// Save email's data into database
        /// </summary>
        /// <param name="item">The email message item</param>
        /// <returns>The email item</returns>
        protected EmailItem SaveEmail(EmailMessage item) {
            if (item == null) return null;
            using (var context = new WorkflowContext()) {
                if (context.Database.SqlQuery<int>(string.Format("SELECT TOP 1 1 FROM EMAIL.MAIL_ITEM WHERE UNIQUE_IDENTIFIER = '{0}' COLLATE Latin1_General_CS_AS ", item.Id.UniqueId)).Count() == 0) {
                    var emailitem = new EmailItem();
                    emailitem.Body = InlineImageUtil.GetHTMLWithInlineImage(item);
                    emailitem.Cc = GetEmailName(item.CcRecipients);
                    emailitem.Originator = item.From.Address;
                    emailitem.Subject = string.IsNullOrEmpty(item.Subject) ? "No Subject" : item.Subject;
                    emailitem.UniqueIdentifier = item.Id.UniqueId;
                    emailitem.Status = EMAIL_STATUS;
                    emailitem.CreatedDate = DateTime.Now;
                    emailitem.Receipient = GetEmailName(item.ToRecipients);
                    emailitem.FileAttachements = new List<FileAttachement>();

                    if (item.HasAttachments) {
                        foreach (var att in item.Attachments) {
                            att.Load();

                            if (att.IsInline) continue;

                            FileAttachment fileAtt = att as FileAttachment;
                            if (fileAtt != null) {
                                MemoryStream memoryStream = new MemoryStream();
                                var fileAttachement = new FileAttachement();
                                fileAttachement.FileName = att.Name;
                                fileAttachement.Ext = Path.GetExtension(att.Name).Replace(".", "");
                                fileAttachement.CreatedDate = DateTime.Now;
                                fileAtt.Load(memoryStream);
                                fileAttachement.DataContent = memoryStream != null ? memoryStream.ToArray() : null;
                                emailitem.FileAttachements.Add(fileAttachement);
                            }
                        }
                    }

                    if (emailitem.FileAttachements.Count == 0) emailitem.FileAttachements = null;

                    context.EmailItems.Add(emailitem);

                    if (context.ChangeTracker.HasChanges()) {
                        context.SaveChanges();
                    }
                    _Logger.Info(emailitem);

                    return emailitem;
                }
            }

            item.IsRead = true;
            item.Update(ConflictResolutionMode.AlwaysOverwrite);

            return null;
        }

        /// <summary>
        /// Get new unread email in inbox folder
        /// </summary>
        public void PullEmail() {
            try {                
                DateTime from = DateTime.Now;
                if (DateTime.TryParse(ConfigurationManager.AppSettings["PullFrom"], out from)) {
                    from = DateTime.Parse(ConfigurationManager.AppSettings["PullFrom"]);
                }

                ExchangeConnect();

                List<SearchFilter> filters = new List<SearchFilter>();
                filters.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));
                filters.Add(new SearchFilter.IsGreaterThanOrEqualTo(EmailMessageSchema.DateTimeCreated, from));

                SearchFilter searchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, filters.ToArray());

                List<Item> findResults = _Emailservice.GetIndoxFormItem(searchFilter, new ItemView(int.MaxValue));
                _Logger.Info(string.Format("search item found = {0}", findResults.Count()));
                foreach (EmailMessage itemMessage in findResults) {
                    var item = _Emailservice.BindToEmailMessage(itemMessage.Id);
                    SaveEmail(item);
                }
            } catch (ServiceRequestException se) {
                _Logger.Error(se.Message, se);
                SendError(se);
            } catch (Exception ex) {
                _Logger.Error(ex.Message, ex);
                SendError(ex);
            }
        }


        /// <summary>
        /// Merge all email to single string
        /// </summary>
        /// <param name="addresses">The email addresses</param>
        /// <returns>The single string of all emails</returns>
        public string GetEmailName(EmailAddressCollection addresses) {
            if (addresses == null || addresses.Count == 0) return string.Empty;
            var emails = addresses.Select(p => p.Address).ToArray();
            return string.Join(";", emails);
        }

        public void Reconnect() {
            try {
                _Logger.Info("reconect start");
                if (_Emailservice != null) {
                    StopNotification();
                    StopSchedule();
                    _Emailservice.Dispose();
                    _Emailservice = null;
                }
                _Emailservice = new EmailService(_UserInfo);
                StartNotification();
                StartSchedule();
                _Logger.Info("reconect completed");
            } catch(Exception ex) {
                _Logger.Error(ex.Message, ex);
                SendError(ex);
            }            
        }

        private void SendError(Exception ex) {
            try {
                string email = ConfigurationManager.AppSettings["Email"] != null ? ConfigurationManager.AppSettings["Email"]: "k2admin@nagaworld.com";
                string password = ConfigurationManager.AppSettings["Password"] != null ? ConfigurationManager.AppSettings["Password"] : "**aa12345";
                string reciepent = ConfigurationManager.AppSettings["Reciepent"] != null ? ConfigurationManager.AppSettings["Reciepent"] : "imsopheap@nagaworld.com";
                List<string> reciepents = new List<string>();
                if (reciepent.Split(',').Count() > 1) {
                    var emails = reciepent.Split(',');
                    foreach(string item in emails) {
                        reciepents.Add(item);
                    }
                } else {
                    reciepents.Add(reciepent);
                }
                var userInfo = UserInfo.CreateUserData(email, password, ExchangeVersion.Exchange2013);
                var emailService = new EmailService(userInfo);
                string message = string.Format("Email: {0}<br />Exception: {1}<br />{2}", _UserInfo.EmailAddress, ex.Message, ex.StackTrace);
                emailService.SendEmail("Ticket Email Service Error", GetEmailContent(message), reciepents);
            } catch(Exception e) {
                _Logger.Error(e.Message, e);
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
                            <h5 style='color:#347D7E; font-weight: bold'>TICKET EMAIL SERVICE ERROR</h5>
                            <br />" + message + @"
                            <br /><br /><span style='font-size:12.0pt;color:#1F497D'>Regards,</span><br /><b><u><span style='font-size:12.0pt;color:#347D7E'>Group IT Department</span></u></b>
	                        <br/><br/><b>Internet E-mail Confidentiality Footer</b><br>This E-mail is confidential and intended only for the use of the individual(s) or entity named above and may contain information that is privileged. If you are not the intended recipient, you are advised that any dissemination, distribution or copying of this E-mail is strictly prohibited. If you have received this E-mail in error, please notify us immediately by return E-mail or telephone and destroy the original message.</p>
                        </body>
                        </html>";
        }
        #endregion        
    }

}
