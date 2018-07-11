using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.Ticketing;
using Workflow.Business.Ticketing.Dto;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities.Ticket;
using Workflow.Service.Ticketing;

namespace Workflow.Service.Interfaces.ticketing
{
    public interface ITicketService
    {
        Object takeAction(ITicketDataParser ticketDataParser);
        Object takeAction(ITicketDataParser ticketDataParser, IActivityMessageHandler actMsgHandler);
        ITicketEnquiry GetTicketEnquiry();
        FileUploadInfo getDownloadInfo(String serial);
        TicketViewDto loadTicket(int id);
        TicketViewDto loadTicket(int id, EmployeeDto emp);
        List<ActionDto> getActions(int ticketId, EmployeeDto emp);
        TicketAssignment getTicketAssignment(int ticketId);

        IEnumerable<TicketItemDto> getTicketItems(string query);
        TicketItem addNewTicketItem(TicketItem item);
        TicketItem updateTicketItem(TicketItem item);
        void deleteTicketItem(int id);

        IEnumerable<TicketSubCategoryDto> getSubCategories(string query = null);
        TicketSubCategory addNewSubCategory(TicketSubCategory subCategory);
        TicketSubCategory updateSubCategory(TicketSubCategory subCategory);
        void deleteSubCategory(int id);

        IEnumerable<TicketCategoryDto> getCategories(string query = null);
        TicketCategory addNewCategory(TicketCategory category);
        TicketCategory updateCategory(TicketCategory category);
        void deleteCategory(int id);

        IEnumerable<TicketAgentDto> getAgents(string query = null);
        TicketAgent addNewAgent(TicketAgent instance);
        TicketAgent updateAgent(TicketAgent instance);
        void deleteAgent(int id);
        IEnumerable<TicketAgentTeamsDto> getAgentTeams(int agentId = 0);
        IEnumerable<TicketTeam> getTeamsByAgent(TicketAgent instance);
        
        IEnumerable<TicketDepartmentDto> getDepartments(string query = null);
        TicketDepartment addNewDepartment(TicketDepartment instance);
        TicketDepartment updateDepartment(TicketDepartment instance);
        void deleteDepartment(int id);

        IEnumerable<TicketGroupPolicyDto> getGroupPolicies(string query = null);
        TicketGroupPolicy addNewGroupPolicy(TicketGroupPolicyDto instance);
        TicketGroupPolicy updateGroupPolicy(TicketGroupPolicyDto instance);
        void deleteGroupPolicy(int id);

        IEnumerable<TicketTeamDto> getTeams(string query = null);        
        TicketTeam addNewTeam(TicketTeam instance);
        TicketTeamDto addNewTeamAgents(TicketTeamDto instance);
        TicketTeam updateTeam(TicketTeam instance);
        TicketTeamDto updateTeamAgents(TicketTeamDto instance);
        void deleteTeam(int id);
        IEnumerable<TicketTeamAgentsDto> getTeamAgents(int teamId = 0);


        IEnumerable<TicketSlaDto> getSlas(string query = null);
        TicketSLA addNewSla(TicketSLA category);
        TicketSLA updateSla(TicketSLA category);
        void deleteSla(int id);        

        IEnumerable<TicketPriorityDto> getPriorities(string query = null);
        TicketPriority addNewPriority(TicketPriority category);
        TicketPriority updatePriority(TicketPriority category);
        void deletePriority(int id);

        IEnumerable<TicketGroupPolicyTeamsDto> getGroupPolicyTeams(int groupPolicyId = 0);
        IEnumerable<TicketGroupPolicyTeamsDto> getGroupPolicyReportAccessTeams(int groupPolicyId = 0);

        object GetReportResult(TicketingSearchParamsDto dto);

        Boolean isItemExisted(TicketItem instance);
        Boolean isSubCategoryExisted(TicketSubCategory instance);
        Boolean isCategoryExisted(TicketCategory instance);
        Boolean isDepartmentExisted(TicketDepartment instance);
        Boolean isGroupPolicyExisted(TicketGroupPolicyDto instance);
        Boolean isAgentExisted(TicketAgent instance);
        Boolean isTeamExisted(TicketTeamDto instance);
        Boolean isSLAExisted(TicketSLA instance);
        Boolean isPriorityExisted(TicketPriority instance);

        IEnumerable<TicketTeamDto> getTeams(TicketSettingCriteria criteria);
        IEnumerable<TicketSubCategoryDto> getSubCategories(TicketSettingCriteria criteria);
        IEnumerable<TicketCategoryDto> getCategories(TicketSettingCriteria criteria);
        IEnumerable<TicketDepartmentDto> getDepartments(TicketSettingCriteria criteria);

        IEnumerable<TicketItemDto> getTicketItems(TicketSettingCriteria criteria);

        IEnumerable<TicketSlaMappingDto> getSlasMapping(string query = null);
        Boolean isSLAMappingExisted(TicketSLAMapping instance);
        TicketSLAMapping addNewSlaMapping(TicketSLAMapping instance);
        TicketSLAMapping updateSlaMapping(TicketSLAMapping instance);
    }


}
