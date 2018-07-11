using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.DataObject.Notification;
using Workflow.DataObject.Ticket;

namespace Workflow.Service.Interfaces.Notification
{
    public interface INotificationService
    {
        int getUnread(EmployeeDto emp);
        List<NotifyDataDto> getNotifyList(EmployeeDto emp);
        void markAsRead(int notifyId);
    }
}
