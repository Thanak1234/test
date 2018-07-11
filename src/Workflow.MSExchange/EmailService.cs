using Workflow.MSExchange.Core;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Thread = System.Threading.Tasks;
using System.Net.Http;
using Workflow.MSExchange;
using RazorEngine.Templating;
using RazorEngine.Configuration;

namespace Workflow.MSExchange {

    public class EmailService : IEmailService {

        #region Data Memeber

        private ExchangeService _exchangeService = null;
        private int lifeTime = 30;
        private List<string> errors = new List<string>();
        private IList<StreamingSubscriptionConnection> connections = new List<StreamingSubscriptionConnection>();

        #endregion

        #region Methods

        /// <summary>
        /// Email service contructor and init Ms Exchange web service connection
        /// </summary>
        public EmailService() {
            UserInfo userInfo = BuildUserInfo();
            CreateConnection(userInfo);
        }

        public EmailService(UserInfo userInfo) {
            CreateConnection(userInfo);
        }

        public EmailService(string userName, string pwd)
        {
            UserInfo userInfo = BuildUserInfo(userName, pwd);
            CreateConnection(userInfo);
        }


        /// <summary>
        /// To create Connection to Microsoft Exchange by User Login and Password
        /// </summary>
        /// <param name="userInfo">The UserInfo is user login information: User name and Password</param>
        private void CreateConnection(UserInfo userInfo) {
            if (userInfo == null) {
                throw new NullReferenceException("Email Information don't in configuration file.");
            }

            try {
                _exchangeService = ExchangeServiceFactory.CreateExchangeService(userInfo);
            } catch (Exception) {
                //throw ex;
            }
        }


        private UserInfo BuildUserInfo(string email, string password)
        {
            ExchangeVersion version = ExchangeVersion.Exchange2013;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                return UserInfo.CreateUserData(email, password, version);
            }

            return null;
        }

        /// <summary>
        /// Build User Information
        /// </summary>
        /// <returns>The User information</returns>
        private UserInfo BuildUserInfo() {
            string email = ConfigurationManager.AppSettings.Get("EmailAdress");
            string password = ConfigurationManager.AppSettings.Get("Password");

            return BuildUserInfo(email, password);
        }

        /// <summary>
        /// Pull notifications specific folders
        /// </summary>
        /// <param name="folderId">The email folder id</param>
        /// <param name="eventTypes">The notification event type</param>
        /// <returns>The Event Result</returns>
        public GetEventsResults PullNotifications(IEnumerable<FolderId> folderId, params EventType[] eventTypes) {
            PullSubscription subscription = _exchangeService.SubscribeToPullNotifications(folderId, lifeTime, null, eventTypes);
            GetEventsResults events = subscription.GetEvents();
            return events;
        }


        /// <summary>
        /// Pull notifications for all folders
        /// </summary>
        /// <param name="eventTypes">The notification event type</param>
        /// <returns>The Event Result</returns>
        public GetEventsResults PullNotifications(params EventType[] eventTypes) {
            PullSubscription subscription = _exchangeService.SubscribeToPullNotificationsOnAllFolders(lifeTime, null, eventTypes);
            GetEventsResults events = subscription.GetEvents();
            return events;
        }

        /// <summary>
        /// Register notification specifice folders
        /// </summary>
        /// <param name="eventTypes">The notification event type</param>
        /// <param name="handler">Callback funtion when notification push from server</param>
        public void RegisterNotification(EventType[] eventTypes, INotificationHandler handler) {
            StreamingSubscriptionConnection connection = CreateNotificationConnection(eventTypes);
            connection.OnNotificationEvent += new StreamingSubscriptionConnection.NotificationEventDelegate(handler.OnNotificationEvent);
            connection.OnSubscriptionError += new StreamingSubscriptionConnection.SubscriptionErrorDelegate(handler.OnNotificationError);
            connection.OnDisconnect += new StreamingSubscriptionConnection.SubscriptionErrorDelegate(handler.OnNotificationDisconnect);
            connection.Open();
        }

        /// <summary>
        /// Register notification for all folders
        /// </summary>
        /// <param name="folders">The folder list</param>
        /// <param name="eventTypes">The notification event type</param>
        /// <param name="handler">Callback funtion when notification push from server</param>
        public void RegisterNotification(FolderId[] folders, EventType[] eventTypes, INotificationHandler handler) {
            StreamingSubscriptionConnection connection = CreateNotificationConnection(folders, eventTypes);
            connection.OnNotificationEvent += new StreamingSubscriptionConnection.NotificationEventDelegate(handler.OnNotificationEvent);
            connection.OnSubscriptionError += new StreamingSubscriptionConnection.SubscriptionErrorDelegate(handler.OnNotificationError);
            connection.OnDisconnect += new StreamingSubscriptionConnection.SubscriptionErrorDelegate(handler.OnNotificationDisconnect);
            connection.Open();
        }

        /// <summary>
        /// create notification connection for all folders
        /// </summary>
        /// <param name="eventTypes">The event type</param>
        /// <returns>The notification streamming connection</returns>
        public StreamingSubscriptionConnection CreateNotificationConnection(params EventType[] eventTypes) {
            StreamingSubscription streamingsubscription = _exchangeService.SubscribeToStreamingNotificationsOnAllFolders(eventTypes);
            StreamingSubscriptionConnection connection = new StreamingSubscriptionConnection(_exchangeService, lifeTime);
            connection.AddSubscription(streamingsubscription);
            connections.Add(connection);
            return connection;
        }

        /// <summary>
        /// create notification connection for subcribing new email from server
        /// </summary>
        /// <param name="folders">folders</param>
        /// <param name="eventTypes">eventTypes</param>
        /// <returns>StreamingSubscriptionConnection</returns>
        public StreamingSubscriptionConnection CreateNotificationConnection(FolderId[] folders, params EventType[] eventTypes) {
            StreamingSubscription streamingsubscription = _exchangeService.SubscribeToStreamingNotifications(folders, eventTypes);
            StreamingSubscriptionConnection connection = new StreamingSubscriptionConnection(_exchangeService, lifeTime);
            connection.AddSubscription(streamingsubscription);
            connections.Add(connection);
            return connection;
        }

        /// <summary>
        /// Bind To EmailItem By Id
        /// </summary>
        /// <param name="id">ItemId</param>
        /// <returns>Item</returns>
        public Item BindToEmailItem(ItemId id) {
            if (id == null) return null;
            Item item = Item.Bind(_exchangeService, id);
            return item;
        }

        /// <summary>
        /// Bind To EmailMessage by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Email Message</returns>
        public EmailMessage BindToEmailMessage(ItemId id) {
            if (id == null) return null;

            PropertySet propSet = new PropertySet(
                BasePropertySet.IdOnly,
                ItemSchema.TextBody,
                EmailMessageSchema.Body,
                ItemSchema.Subject,
                EmailMessageSchema.From,
                EmailMessageSchema.Attachments,
                EmailMessageSchema.HasAttachments,
                EmailMessageSchema.DateTimeCreated,
                EmailMessageSchema.CcRecipients,
                EmailMessageSchema.BccRecipients,
                EmailMessageSchema.DisplayCc,
                EmailMessageSchema.DisplayTo,
                EmailMessageSchema.ToRecipients
                );

            propSet.RequestedBodyType = BodyType.HTML;

            EmailMessage msg = EmailMessage.Bind(_exchangeService, id, propSet);
            return msg;
        }

        /// <summary>
        /// Find email by query string
        /// url: https://msdn.microsoft.com/en-us/library/ee693615.aspx
        /// </summary>
        /// <param name="query">for example: "Body:Exchange"</param>
        /// <param name="options">options is ItemView</param>
        /// <returns>The Email Message</returns>
        public FindItemsResults<Item> Find(SearchFilter searchFilter, ItemView options = null) {

            if (searchFilter == null)
                return null;

            if (options == null) {
                options = new ItemView(9);
            }

            FindItemsResults<Item> results = _exchangeService.FindItems(WellKnownFolderName.Inbox, searchFilter, options);
            return results;
        }

        /// <summary>
        /// Find email by query string
        /// url: https://msdn.microsoft.com/en-us/library/ee693615.aspx
        /// </summary>
        /// <param name="query">for example: "Body:Exchange"</param>
        /// <param name="options">options is ItemView</param>
        /// <returns>The Email Message</returns>
        public FindItemsResults<Item> Find(string query, ItemView options = null) {

            if (string.IsNullOrEmpty(query))
                return null;

            if (options == null) {
                options = new ItemView(9);
            }

            FindItemsResults<Item> results = _exchangeService.FindItems(WellKnownFolderName.Inbox, query, options);
            return results;
        }

        public List<Item> GetIndoxFormItem(SearchFilter searchFilter, ItemView options = null) {
            List<Item> items = new List<Item>();
            List<Folder> folderIds = new List<Folder>();
            FolderView folderView = new FolderView(int.MaxValue);
            FindFoldersResults findFolderResults = _exchangeService.FindFolders(WellKnownFolderName.Inbox, folderView);

            if(findFolderResults != null && findFolderResults.TotalCount > 0) {                
                FindItemsResults<Item> parents = Find(searchFilter, options);
                foreach (Item item in parents) {
                    items.Add(item);
                }

                Folder folder = findFolderResults.Folders[0];
                FindItemsResults<Item> childrends = _exchangeService.FindItems(folder.Id, searchFilter, options);
                foreach (Item item in childrends) {
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// Attachements Extract from EmailMessage
        /// </summary>
        /// <param name="email">The Email Message</param>
        /// <param name="folder">The specific email folder</param>
        public void AttachementsEx(EmailMessage email, string folder) {
            if (email == null || string.IsNullOrEmpty(folder)) return;
            email.Load(new PropertySet(EmailMessageSchema.Attachments));
            foreach (Attachment attachment in email.Attachments) {
                if (attachment is FileAttachment) {
                    FileAttachment fileAttachment = attachment as FileAttachment;
                    fileAttachment.Load();
                    string fullPath = string.Format("{0}\\{1}", folder, fileAttachment.Name);
                    using (FileStream theStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
                        fileAttachment.Load(theStream);
                    }
                }
            }
        }


        /// <summary>
        /// Extract attachements file asyncronous
        /// </summary>
        /// <param name="email">The email address</param>
        /// <param name="folder">The folder path</param>
        /// <returns>The asyncronous task</returns>
        public Thread.Task AttachementsExAsync(EmailMessage email, string folder) {
            return Thread.Task.Factory.StartNew(() => {
                AttachementsEx(email, folder);
            });
        }


        /// <summary>
        /// Send email message with attachements(optional)
        /// </summary>
        /// <param name="subject">subject</param>
        /// <param name="body">body</param>
        /// <param name="to">to</param>
        /// <param name="cc">cc</param>
        /// <param name="bcc">bcc</param>
        /// <param name="attachements">The attachement objects</param>
        /// <returns>(True) sent success or (False) sent fail</returns>
        public bool SendEmail(string subject, string body, List<string> to, List<string> cc = null, List<string> bcc = null, List<EmailFileAttachment> attachements = null, object model = null) {
            EmailMessage message = new EmailMessage(_exchangeService);
            message.Subject = subject;

            string bodyContent = body;

            if (model != null) {
                var config = new TemplateServiceConfiguration {
                    BaseTemplateType = typeof(HtmlTemplateBase<>),
                };
                var templateService = new TemplateService(config);
                bodyContent = templateService.Parse(body, model, null, null);
            }

            message.Body = new MessageBody(BodyType.HTML, bodyContent);


            if (to != null && to.Count > 0) {
                to.ForEach(x => {
                    if (!string.IsNullOrEmpty(x)) {
                        message.ToRecipients.Add(x);
                    }
                });
            }

            if (cc != null && cc.Count > 0) {
                cc.ForEach(x => {
                    if (!string.IsNullOrEmpty(x)) {
                        message.CcRecipients.Add(x);
                    }
                });
            }

            if (bcc != null && bcc.Count > 0) {
                bcc.ForEach(x => {
                    if (!string.IsNullOrEmpty(x)) {
                        message.BccRecipients.Add(x);
                    }
                });
            }

            if (attachements != null && attachements.Count > 0) {
                attachements.ForEach(x => {
                    if (!string.IsNullOrEmpty(x.Name)) {
                        if (x.isBinaryFile) {
                            message.Attachments.AddFileAttachment(x.Name, x.ByteFile);
                        } else {
                            if (File.Exists(x.File)) {
                                message.Attachments.AddFileAttachment(x.Name, x.File);
                            }
                        }

                    }
                });
            }

            try {
                message.SendAndSaveCopy();
            } catch (Exception ex) {
                errors.Add(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Send email message by email data instance
        /// </summary>
        /// <param name="data">The email data instance</param>
        /// <returns>(True) sent success or (False) sent fail</returns>
        public bool SendEmail(IEmailData data) {
            return SendEmail(data.Subject, data.Body, data.Recipients, data.Ccs, data.Bccs, data.AttachmentFiles, data.Model);
        }

        public void DestroyAllConnection() {
            if (connections != null && connections.Count > 0) {
                foreach (var conn in connections) {
                    if (conn.IsOpen) {
                        conn.Close();
                    }
                }
            }
        }

        public void Dispose() {
            DestroyAllConnection();
        }

        #endregion
    }
}
