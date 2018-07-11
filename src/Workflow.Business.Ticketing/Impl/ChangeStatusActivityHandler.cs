/**
*@author : Phanny
*/
using HandlebarsDotNet;
using System;
using Workflow.Business.Ticketing.Dto;
using Workflow.Domain.Entities.Ticket;
using System.Collections.Generic;

namespace Workflow.Business.Ticketing.Impl
{
    public class ChangeStatusActivityHandler : AbstractTicketActivityHandler<ChangeStatusActParams>, ITicketActivityHandler
    {

        public const string ACTIVITY_CODE = "CHANGE_STATUS";
        private string flag;
        private bool isUpladLastAct;
        private TicketStatus oldStatus;
        private TicketStatus toStatus;

        public ChangeStatusActivityHandler(IDataProcessingProvider dataProcessingProvider, ChangeStatusActParams ticketParam) : base(dataProcessingProvider, ticketParam) {
            toStatus = dataProcessingProvider.getStatus(ticketParam.StatusId);
            oldStatus = dataProcessingProvider.getStatus(ticket.StatusId);
        }

        public ChangeStatusActivityHandler(string flag, Ticket ticket, IDataProcessingProvider dataProcessingProvider, ChangeStatusActParams ticketParam,List<IActivityMessageHandler> actMssgHandlers) : base(dataProcessingProvider, ticketParam)
        {
            this.flag = flag;
            isUpladLastAct = TicketActivityHandler.ACTIVITY_CODE.Equals(flag) || false;
            base.ticket = ticket;
            base.actMssgHandlers = actMssgHandlers;
            toStatus = dataProcessingProvider.getStatus(ticketParam.StatusId);
            oldStatus = dataProcessingProvider.getStatus(ticket.StatusId);
        }
        protected override ActivityConfig getActivityConfig()
        {
            
            return new ActivityConfig()
            {
                ActivityName = "Change Status",
                ActivityCode = ACTIVITY_CODE,
                Action = getActionName(),
                HasComment = REQURIED.REQUIRED,
                NotifyType = getNotificationType(),
                FirstResponseMarked = firstActionMarked ()
            };
        }


        private string getActionName()
        {
            return string.Format("Change status from {0} to {1}",oldStatus.Status , toStatus.Status) ;
        }

        private bool firstActionMarked()
        {
            return ticketParam.StatusId >1 ;
        }

        private NOTIFICATION_TYPE getNotificationType()
        {
           

            if (toStatus.StateId == 2)
            {
                return NOTIFICATION_TYPE.EMAIL_UI;
            }
            else
            {
                return NOTIFICATION_TYPE.NONE;
            }
        }


        protected override string getActivityType()
        {
            return ACTIVITY_CODE;
        }
        
        protected override void preProcessing()
        {
            if (ticketParam.StatusId == 0)
            {
                throw new Exception("Status is required.");
            }

            if(ticketParam.StatusId == ticket.StatusId && !TicketActivityHandler.ACTIVITY_CODE.Equals(flag))
            {
                throw new Exception("Please select the different one.");
            }



            //Make sure all sub tickets is not active.
            var ticketStatus = dataProcessingProvider.getStatus(ticketParam.StatusId);

            if (ticketStatus.StateId != 1 && !dataProcessingProvider.noActiveSubticket(ticket.Id))
            {
                throw new Exception("Please make sure all sub tickets are not active.");
            }

            //this.oldStatusId = ticket.StatusId;

            //Auto assign
            var assignedAgent = dataProcessingProvider.getAssigedAgent(ticket.Id);
            if (assignedAgent == null && dataProcessingProvider.isTeamMenber(ticket.LastAssTeamId,getCurrLoginAgent().Id)) //assigned to current login agent
            {
                autoAssigned(getCurrLoginAgent());
            }


        }

        protected override void postProcessing()
        {

            var changeStatus = new TicketChangeActivity()
            {
                ActivityId = ticketActivity.Id,
                statusFromId = this.oldStatus.Id,
                statusToId = ticketParam.StatusId,
                RootCauseId = ticketParam.RootCauseId                
            };
            dataProcessingProvider.saveChangeStatus(changeStatus);
            
            var oldStatus = dataProcessingProvider.getStatus(ticket.StatusId);

            ticket.StatusId = ticketParam.StatusId;
            
            if (toStatus.StateId != 1)
            {
                ticket.WaitActionedBy = null;
            }

            if (oldStatus.StateId != 1 && toStatus.StateId == 1)
            {
                ticket.WaitActionedBy = "Agent";
            };

            if(toStatus.StateId ==2 || toStatus.StateId==3)
            {
                ticket.actualMinutes = ticketParam.ActualMinutes;
                ticket.completedDate = DateTime.Now;
                ticket.RootCauseId = ticketParam.RootCauseId;
            }

            if (ticket.FirstResponseDate == null)
            {
                ticket.FirstResponseDate = DateTime.Now;
            }
            //dataProcessingProvider.saveTicket(ticket);
        }


        private void autoAssigned(TicketAgent tAgent)
        {
            //Auto assign
            AssignedTicketParams assignedTicketParams = new AssignedTicketParams()
            {
                TicketId = ticketParam.TicketId,
                ActivityCode = AssignTicketActivityHandler.ACTIVITY_CODE,
                ActionCode = "Auto Assigned",
                TeamId = ticket.LastAssTeamId,
                Assignee = tAgent.Id,
                ActComment = "Auto assigned since agent take [Change Status] action.",
                bySystem = true

            };
            ITicketActivityHandler assinged = new AssignTicketActivityHandler(ACTIVITY_CODE, ticket, dataProcessingProvider, assignedTicketParams, actMssgHandlers);
            assinged.takeAction();
        }

        protected override string getBody()
        {

            var html = @"Dear {{recepient}}, <br/><br/>

                        {{bodyHeader}}<br/>
                        <h2>Activity Summary</h2>
                        <p><strong>Activity</strong> : {{activity}}</p>
                        <p><strong>Action </strong>: {{action}}</p>
                        <p><strong>By </strong>: {{enpName}}({{empNo}})</p>
                        <p><strong>Action Date </strong>: {{createdData}}</p>
                        <p><strong>Comment</strong>: </p>
                        <div style='line-height:1.4;border-left:2px solid #009688;margin-left:20'><div style ='margin-left:15'>" + ticketParam.ActComment + "</div></div> <br/><strong>{{signature}}</strong><br/><br/>";

            html += "<b><span style='font-size:10.0pt;font-family:&quot;ITC Stone Sans Std Medium&quot;; color:navy'>Internet E-mail Confidentiality Footer</span></b>  <p class='MsoNormal'><i><span style='font-family:&quot;ITC Stone Sans Std Medium&quot;;color:navy'>This E-mail is confidential and intended only for the use of the individual(s) or entity named above and may contain information that is privileged.&nbsp; If you are not the intended recipient, you are advised that any dissemination, distribution or copying of this E-mail is strictly prohibited. If you have received this E-mail in error, please notify us immediately by return E-mail or telephone and destroy the original message.</span></i><o:p></o:p></p>";

            var template = Handlebars.Compile(html);
            var emp = dataProcessingProvider.getEmployee(ticketParam.CurrLoginUserId);
            var data = new
            {
                recepient = getMailTo(),
                bodyHeader = getBodyHeader(),
                activity = activityConfig.ActivityName,
                action = activityConfig.Action,
                createdData = String.Format("{0:d/M/yyyy HH:mm:ss}", ticketActivity.CreatedDate),
                enpName = emp.DisplayName,
                empNo = emp.EmpNo,
                signature = getSinature()
            };

            html = template(data);
            return html;
        }

        protected override string getBodyHeader()
        {
            return "Your ticket have been closed. Please find the information below:";
        }
    }
}
