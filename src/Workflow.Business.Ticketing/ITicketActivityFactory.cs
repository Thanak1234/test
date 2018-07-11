/**
*@author : Phanny
*/
using Workflow.Business.Ticketing.Dto;

namespace Workflow.Business.Ticketing
{
    public interface ITicketActivityFactory 
    {
        ITicketActivityHandler getTicketActivityHandler(IDataProcessingProvider dataProcessingProvider, AbstractTicketParam ticketParams);
    }
}
