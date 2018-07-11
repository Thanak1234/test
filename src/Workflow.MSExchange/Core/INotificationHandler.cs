using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.MSExchange.Core {

    public interface INotificationHandler {
        void OnNotificationEvent(object sender, NotificationEventArgs args);
        void OnNotificationError(object sender, SubscriptionErrorEventArgs args);
        void OnNotificationDisconnect(object sender, SubscriptionErrorEventArgs args);
    }

}
