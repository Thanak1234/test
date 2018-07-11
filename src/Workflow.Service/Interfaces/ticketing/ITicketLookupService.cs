using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.Service.Interfaces
{
    public interface ITicketLookupService
    {
        IEnumerable<TicketType> listTicketType();

        IEnumerable<TicketType> listTicketType(bool includeUnidentified);

        /**
            All = true, retrieve all status
            All = False, disclude removed and merged status
        */
        IEnumerable<TicketStatus> listTicketStatus(bool all);
        IEnumerable<TicketStatus> listTicketStatus(int selectedState, int currStatusId);
        IEnumerable<TicketStatus> listTicketStatus(TicketingLookupParamsDto param);

        IEnumerable<TicketSource> listTicketSource();
        IEnumerable<TicketImpact> listTicketImpact();
        IEnumerable<TicketUrgency> listTicketUrgency();
        IEnumerable<TicketPriority> listTicketPriority();
        IEnumerable<TicketSite> listTicketSite();
        IEnumerable<TicketSLA> listTicketSLA();
        IEnumerable<TicketCategory> listTicketCategory();
        IEnumerable<TicketSubCategory> listTicketSubCate();
        IEnumerable<GeneralLookupDto> listTicketSubCate(int cateId, bool breadcrumb);
        IEnumerable<GeneralLookupDto> listTicketSubCate(IEnumerable<int> cateId, bool breadcrumb);

        IEnumerable<TicketItem> listTicketItem();
        IEnumerable<TicketDepartment> listTicketDept();

        IEnumerable<TicketTeam> listTicketTeam();

        IEnumerable<GeneralLookupDto> listTicketItem(int subCateId);
        IEnumerable<GeneralLookupDto> listTicketItem(IEnumerable<int> cateId, IEnumerable<int> subCateId, bool breadcrumb);

        IEnumerable<TicketAgent> listTicketAgent();

        //IEnumerable<GeneralLookupDto> listTicketTeam(int ticketId);
        IEnumerable<GeneralLookupDto> listTicketAgent(int teamId);

        IEnumerable<GeneralLookupDto> listTicketByKey(string lookupKey);

        IEnumerable<GeneralLookupDto> listTicketGroupPolicy();
        IEnumerable<GeneralLookupDto> listTicketDepartment();
        IEnumerable<GeneralLookupDto> listTicketStatus();
        IEnumerable<GeneralLookupDto> listTicketAccountType();
        IEnumerable<GeneralLookupDto> GetAgentEmpIdByTeam(int teamId);
        IEnumerable<GeneralLookupDto> GetAgentEmpIdByTeam(IEnumerable<int> teamId);
    }
}
