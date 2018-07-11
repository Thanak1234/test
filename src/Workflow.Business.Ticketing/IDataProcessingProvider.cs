/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Email;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.Business.Ticketing
{
    public interface IDataProcessingProvider
    {
        Ticket getTicket(int ticketId);
        TicketViewDto getTicketView(int ticketId);
        List<SimpleActivityViewDto> getActivity(int ticketId, EmployeeDto emp);
        List<SimpleActivityViewDto> getActivity(List<int> actIds);
        NotifyActivityDataDto getActivity(int activityId);

        List<TicketFileUpload> getFileUpload(int activityId);
        TicketAssignInfo getAssingeeInfo(int activityId);
        List<ActionDto> getAvailableActions(int ticketStateId, int agentId);
        FormIntegratedDto getFormIntegrated(int ticketId);
        
        int saveTicket(Ticket ticket);
        int createTicketActivity(TicketActivity ticketActivity);
        void saveAttachmentFiles(List<String> fileSerials,TicketActivity ticketActivity);
        void saveTicketAssignment(TicketAssignment ticketAssignment);
        void saveTicketRouting(TicketRouting ticketRouting);
        void expiredCurrAssigned(Ticket ticket);
        void saveChangeStatus(TicketChangeActivity changeStatusAct);
        void saveMergedTicket(TicketMerged ticketMerged);
        void saveSubTicketLink(TicketSubTkLink ticketSubTkLink);
        void saveNotificatin(TicketNotification notification);
        void saveTicketNoneReqEmp(TicketNoneReqEmp ticketNoneReqEmp);

        //int getDefaultTeamIdByTicketItem(int ticketItemId);
        TicketItem getTicketItem(int ticketItemId);
        int getDefaultAgentByTeamId(int teamId);
        bool isAgent(int empId);
        TicketAgent getAgentByEmpId(int empId);
        TicketAgent getAgentById(int agentId);
        int getAssignee(int ticketId);

        TicketAgent getAssigedAgent(int ticketId);
        
        //Not include first assigned
        List<int> getDestUsersByTeam(int teamId);
        
        int getDefaultTicketItem(int deptId);
        int getDefaultDepartmentId();


        string getLastTicketNo();
        string getChangeStatusDesc(int ticketActId);

        TicketStatus getStatus(int statusId);

        TicketRouting getCurrUsrRouting(int ticketId, int userId);
        TicketAssignment getCurrentAssigned(int ticketId);

        void releaseTicket(int ticketId);

        //Func<TicketActivityParams,int> AddActivityFun { get; set; }

        void saveFileUploads(List<FileUploadInfo> fileInfoList, TicketActivity ticketActivity);

        Employee getEmployee(int empId);
        Employee getUnknowRequestor(Ticket ticket);

        List<Employee> getTeamMember(int teamId);
        List<AgentInfo> getAgentInfoByTeam(int teamId);
        bool isTeamMenber(int teamId, int agentId);

        List<TicketDepartment> getDepts();
        TicketDepartment getDept(int deptId);

        TicketMergeStatusDto getMergeInfo(int activityId);
        TicketListing getSubTitket(int activityId);
        TicketListing getMainTicket(int activityId);

        TicketSLA getSLAByPriority(int priorityId);

        bool canAccessAsAgent(int agentId, int teamId);

        bool noActiveSubticket(int mainTicketId);

        void markAsRead(int notifyId);

        MailList getMailConfig(string email);

        TicketSLA getSLAByPriorityAndTicketType(int priorityId, int ticketTypeId);

        object GetEmailItem(int? id);
        string GetITFormContent(int id);
    }

    public class Id
    {
        public int id { get; set; }
    }
}
