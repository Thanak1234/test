/**
*@author : Phanny
*/
using System.Collections.Generic;
using Workflow.DataContract.K2;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;
using Workflow.DataObject.Worklists;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.Service.Ticketing
{
    public interface ITicketEnquiry
    {
        //Query 
        //TODO: add more parameters

        QueryResult getTicketListing(string keyword, int status ,string quickQuery,EmployeeDto emp, int execptTecktId, int page = 0, int start = 0, int limit = 0, List<int?> ticketTypeId = null, string sort = null);
        TicketFileUpload getFileInfo(string serial);
        TicketStatus GetCurStatusByTicketId(int ticketId);
        List<TicketListing> GetSubtickets(int ticketId);
        TicketPriority getPriority(int impactId, int urgencyId);

        bool isAgent(EmployeeDto emp);
        List<TicketItemDashboard> getTicketItemDashboard(TicketItemDashboard.TICKET_ITEM_DHB type, EmployeeDto emp);

        List<HierarchyDashB> getItemPerformance(int type, HierarchyDashB.TIME_FRAME timeFrame, HierarchyDashB.TIME_FILTER timeFilter  , EmployeeDto emp);

        int getUnreadNotify(EmployeeDto emp);

        List<TKNotifyDto> getNotificationList(EmployeeDto emp);

        ProcInst GetWorkListItem(int ticketId, string loginUser);
        TicketSLA getSla(int typeId, int priorityId);
        IEnumerable<TicketSLA> getSlas(IEnumerable<int> typeId, IEnumerable<int> priorityId);
    }
}
