/**
*@author : Phanny
*/
using Workflow.Business.Ticketing.Dto;
using Workflow.DataObject.Ticket;

namespace Workflow.Service.Ticketing
{
    public interface ITicketDataParser
    {
        AbstractTicketParam parse();
        ActivityDto getActivityDto();
    }
}
