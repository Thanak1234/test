using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using Workflow.MSExchange.Core;
using Thread = System.Threading.Tasks;

namespace Workflow.MSExchange
{

    public interface IEmailService: IDisposable {
        GetEventsResults PullNotifications(params EventType[] eventTypes);
        GetEventsResults PullNotifications(IEnumerable<FolderId> folderId, params EventType[] eventTypes);
        void RegisterNotification(EventType[] eventTypes, INotificationHandler handler);
        void RegisterNotification(FolderId[] folders, EventType[] eventTypes, INotificationHandler handler);
        StreamingSubscriptionConnection CreateNotificationConnection(params EventType[] eventTypes);
        StreamingSubscriptionConnection CreateNotificationConnection(FolderId[] folders, params EventType[] eventTypes);
        FindItemsResults<Item> Find(SearchFilter searchFilter, ItemView options = null);
        FindItemsResults<Item> Find(string query, ItemView options = null);
        void AttachementsEx(EmailMessage email, string folder);
        Thread.Task AttachementsExAsync(EmailMessage email, string folder);
        Item BindToEmailItem(ItemId id);
        EmailMessage BindToEmailMessage(ItemId id);
        bool SendEmail(string subject, string body, List<string> to, List<string> cc=null, List<string> bcc=null, List<EmailFileAttachment> attachements=null, object model = null);
        bool SendEmail(IEmailData data);
        void DestroyAllConnection();
        List<Item> GetIndoxFormItem(SearchFilter searchFilter, ItemView options = null);
    }
}
