/**
*@author : Phanny
*/
using Workflow.Business.Ticketing.Dto;

namespace Workflow.Business.Ticketing
{
    public interface ITicketHandler
    {
        void setTicketParams(TicketParams ticketParams);

        void createTicket();
        void editTicket();
        void removeTicket();

    }
}
