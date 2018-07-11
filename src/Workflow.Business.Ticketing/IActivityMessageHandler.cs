/**
*@author : Phanny
*/
using Workflow.Business.Ticketing.Dto;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.Business.Ticketing
{
    public interface IActivityMessageHandler
    {
        void onTicketCreation(Ticket ticket);
        void onActivityCreation(TicketActivity activity, AbstractTicketParam tkParam);
    }
}
