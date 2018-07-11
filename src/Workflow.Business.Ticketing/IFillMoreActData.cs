/**
*@author : Phanny
*/
using Workflow.DataObject.Notification;
using Workflow.DataObject.Ticket;

namespace Workflow.Business.Ticketing
{
    public interface IFillMoreActData
    {
        void fill(TicketViewDto ticket, SimpleActivityViewDto act);
        void fill(SimpleActivityViewDto act);
        void fill(TicketViewDto ticket);
        NotifyDataDto fill(TKNotifyDto notifyData);
    }
}
