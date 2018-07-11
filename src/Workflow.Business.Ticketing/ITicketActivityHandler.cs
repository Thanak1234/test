/**
*@author : Phanny
*/

namespace Workflow.Business.Ticketing
{
    public interface ITicketActivityHandler
    {
        void takeAction();
        void setNotifyHandler(ITicketNotifyHandler notifyHandler);
        void addActMsgHandler(IActivityMessageHandler handler);
    }
}
