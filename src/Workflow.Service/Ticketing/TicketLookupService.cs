/**
*@author : Phanny
*/
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.ticket;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities.Ticket;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class TicketLookupService : ITicketLookupService
    {
        private IDbFactory dbFactory = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
        private ITicketLookupRepository ticketLookupRepo = null;

        public TicketLookupService ()
        {
             ticketLookupRepo =new TicketLookupRepository(dbFactory);
        }

        public IEnumerable<TicketAgent> listTicketAgent()
        {
            return ticketLookupRepo.listTicketAgent();
        }

        public IEnumerable<GeneralLookupDto> listTicketAgent(int teamId)
        {
            return ticketLookupRepo.listTicketAgent(teamId);
        }

        public IEnumerable<GeneralLookupDto> GetAgentEmpIdByTeam(int teamId) {
            return ticketLookupRepo.GetAgentEmpIdByTeam(teamId);
        }

        public IEnumerable<GeneralLookupDto> GetAgentEmpIdByTeam(IEnumerable<int> teamId)
        {
            return ticketLookupRepo.GetAgentEmpIdByTeam(teamId);
        }

        public IEnumerable<TicketCategory> listTicketCategory()
        {
            return ticketLookupRepo.listTicketCategory();
        }

        public IEnumerable<TicketDepartment> listTicketDept()
        {
            return ticketLookupRepo.listTicketDept();
        }

        public IEnumerable<TicketImpact> listTicketImpact()
        {
            return ticketLookupRepo.listTicketImpact();
        }

        public IEnumerable<TicketItem> listTicketItem()
        {
            return ticketLookupRepo.listTicketItem();
        }

        public IEnumerable<TicketPriority> listTicketPriority()
        {
            return ticketLookupRepo.listTicketPriority();
        }

        public IEnumerable<TicketSite> listTicketSite()
        {
            return ticketLookupRepo.listTicketSite();
        }

        public IEnumerable<TicketSLA> listTicketSLA()
        {
            return ticketLookupRepo.listTicketSLA();
        }

        public IEnumerable<TicketSource> listTicketSource()
        {
            return ticketLookupRepo.listTicketSource();
        }

        public IEnumerable<TicketStatus> listTicketStatus(bool all)
        {
            return ticketLookupRepo.listTicketStatus(all);
        }

        public IEnumerable<TicketStatus> listTicketStatus(int selectedState, int currStatusId)
        {
            return ticketLookupRepo.listTicketStatus(selectedState, currStatusId);
        }

        public IEnumerable<TicketStatus> listTicketStatus(TicketingLookupParamsDto param)
        {
            return ticketLookupRepo.listTicketStatus(param);
        }

        public IEnumerable<TicketSubCategory> listTicketSubCate()
        {
            return ticketLookupRepo.listTicketSubCate();
        }

        public IEnumerable<GeneralLookupDto> listTicketSubCate(int cateId, bool breadcrumb)
        {
            return ticketLookupRepo.listTicketSubCate(cateId, breadcrumb);
        }

        public IEnumerable<GeneralLookupDto> listTicketSubCate(IEnumerable<int> cateId, bool breadcrumb)
        {
            return ticketLookupRepo.listTicketSubCate(cateId, breadcrumb);
        }

        public IEnumerable<TicketTeam> listTicketTeam()
        {
            return ticketLookupRepo.listTicketTeam();
        }

        public IEnumerable<GeneralLookupDto> listTicketItem(int subCateId)
        {
            return ticketLookupRepo.listTicketItem(subCateId);
        }

        public IEnumerable<GeneralLookupDto> listTicketItem(IEnumerable<int> cateId, IEnumerable<int> subCateId, bool breadcrumb)
        {
            return ticketLookupRepo.listTicketItem(cateId, subCateId, breadcrumb);
        }

        public IEnumerable<TicketType> listTicketType()
        {
            return ticketLookupRepo.GetAll();
        }

        public IEnumerable<TicketType> listTicketType(bool includeUnidentified)
        {
            return ticketLookupRepo.listTicketType(includeUnidentified);
        }

        public IEnumerable<TicketUrgency> listTicketUrgency()
        {
            return ticketLookupRepo.listTicketUrgency();
        }

        public IEnumerable<GeneralLookupDto> listTicketByKey(string lookupKey)
        {
            return ticketLookupRepo.listTicketByKey(lookupKey);
        }

        public IEnumerable<GeneralLookupDto> listTicketGroupPolicy()
        {
            return ticketLookupRepo.listTicketGroupPolicy();
        }
        public IEnumerable<GeneralLookupDto> listTicketDepartment()
        {
            return ticketLookupRepo.listTicketDepartment();
        }
        public IEnumerable<GeneralLookupDto> listTicketStatus()
        {
            return ticketLookupRepo.listTicketStatus();
        }
        public IEnumerable<GeneralLookupDto> listTicketAccountType()
        {
            return ticketLookupRepo.listTicketAccounttype();
        }
    }
}
