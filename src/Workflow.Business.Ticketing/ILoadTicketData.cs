/**
*@author : Phanny
*/
using System.Collections.Generic;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;

namespace Workflow.Business.Ticketing
{
    public interface ILoadTicketData
    {
        TicketViewDto getTicketView(int id, EmployeeDto emp);
        List<ActionDto> getActions(int id, EmployeeDto emp, TicketViewDto ticketViewDto = null);
    }
}
