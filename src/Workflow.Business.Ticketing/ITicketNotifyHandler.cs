/**
*@author : Phanny
*/
using Workflow.Business.Ticketing.Dto;

namespace Workflow.Business.Ticketing
{
    public interface ITicketNotifyHandler
    {

        void notify(AbstractNotifyData notifyData);
    }
}
