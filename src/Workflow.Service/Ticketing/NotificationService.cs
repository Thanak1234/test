using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.Ticketing;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject;
using Workflow.DataObject.Notification;
using Workflow.DataObject.Ticket;
using Workflow.Service.Interfaces.Notification;
using Workflow.Service.Ticketing;

namespace Workflow.Service.Notification
{
    public class NotificationService : INotificationService
    {

        private readonly ITicketEnquiry ticketEnquiry;
        private readonly IDataProcessingProvider dataProcessingProvider;

        public NotificationService()
        {
            var dbContext = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            dataProcessingProvider = new TicketDataProcessingProvider(dbContext);
            ticketEnquiry = new TicketDataProcessingProvider(dbContext);
        }

        public List<NotifyDataDto> getNotifyList(EmployeeDto emp)
        {

            IFillMoreActData fillData = new FillMoreActData(dataProcessingProvider);

            var list = ticketEnquiry.getNotificationList(emp);
            var dataList = new List<NotifyDataDto>();

            list.ForEach(t =>
            {
                dataList.Add(fillData.fill(t));
            });

            return dataList;
        }

        public int getUnread(EmployeeDto emp)
        {
            return ticketEnquiry.getUnreadNotify(emp);
        }

        public void markAsRead(int notifyId)
        {
            dataProcessingProvider.markAsRead(notifyId);
        }
    }
}
