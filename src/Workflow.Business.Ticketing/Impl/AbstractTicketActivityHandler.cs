/**
*@author : Phanny
*/
using HandlebarsDotNet;
using System;
using System.Collections.Generic;
using Workflow.Business.Ticketing.Dto;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.Business.Ticketing.Impl
{
    public abstract class AbstractTicketActivityHandler<T>  where T : AbstractTicketParam
    {
        public enum ACTIVITY_TYPE
        {
            POST_TICKET =1 ,
            ASSIGNED,
            POST_REPLY,
            INTERNAL_NOTE,
            CHANGE_STATUS
        }
        protected IDataProcessingProvider dataProcessingProvider;
        protected T ticketParam;
        private Dto.ActivityConfig _activityConfig = null;
        protected Dto.ActivityConfig activityConfig {
            get {
                if (_activityConfig == null) {
                    _activityConfig = getActivityConfig();

                    //this.activityConfig = getActivityConfig();
                    if (this._activityConfig == null)
                    {
                        throw new Exception("Activity configuration is not implemented.");
                    }

                }

                return _activityConfig;
            }
        }
        protected List<IActivityMessageHandler> actMssgHandlers =null;

        //Data

        protected Ticket ticket;
        protected TicketActivity ticketActivity;


        protected Employee requestor = null;
        protected Employee submitter = null;
        protected Employee currLogin = null;
        protected TicketDepartment ticketDept = null;

        protected const string WAIT_ACTIONED_BY_AGENT = "AGENT";
        protected const string WAIT_ACTIONED_BY_USER = "USER";
        


        //Store necessary information
        //protected bool loginAsAgent =false;
        private TicketAgent currAgent = null;

        protected ITicketNotifyHandler notifyHandler;

        public void addActMsgHandler(IActivityMessageHandler handler)
        {

            if(actMssgHandlers == null)
            {
                actMssgHandlers = new List<IActivityMessageHandler>();
            }
            
            actMssgHandlers.Add(handler);
        }

        protected void onTicketHandler(Ticket ticket)
        {
            actMssgHandlers.ForEach(t => t.onTicketCreation(ticket));
        }

        protected void onActivityHandler(TicketActivity ticketActivity,AbstractTicketParam tkParam)
        {
            actMssgHandlers.ForEach(t => t.onActivityCreation(ticketActivity, tkParam));
        }

        public AbstractTicketActivityHandler(IDataProcessingProvider dataProcessingProvider, T ticketParam)
        {
            this.dataProcessingProvider = dataProcessingProvider;
            this.ticketParam = ticketParam;
            this.currAgent = dataProcessingProvider.getAgentByEmpId(ticketParam.CurrLoginUserId);
            loadTicket();
            loadInfomation();

        }

        public void takeAction()
        {

            validate();
            
            preProcessing();

            if (ACTIVITY_OP.SAVE == activityConfig.Opr)
            {
                addActivity();
            }
            else if (ACTIVITY_OP.EDIT == activityConfig.Opr)
            {
                //TODO: to be implimented
            }
            else if (ACTIVITY_OP.DELETE == activityConfig.Opr)
            {
                //TODO: to be implimented
            }
            postProcessing();

            if (isUpladLastActivity())
            {
                lastUpdate();
               // dataProcessingProvider.commit();
            }
            
            notify();
            


        }

        protected virtual void validate()
        {
            var comment = ticketParam.ActComment;
            if (activityConfig.HasComment == REQURIED.REQUIRED && string.IsNullOrWhiteSpace(comment))
            {
                throw new Exception(String.Format("User comment is required, Activity: {0}", activityConfig.ActivityName));
            }

        }

        private void loadTicket()
        {
            if ( ticket==null && ticketParam.TicketId > 0  )
            {
                ticket = dataProcessingProvider.getTicket(ticketParam.TicketId);
            }
            
        }

        protected virtual void addActivity()
        {

            TicketActivity ticketActivity = new TicketActivity()
            {
                Description = ticketParam.ActComment,
                ActivityType = getActivityType(),
                Action = activityConfig.Action,
                ActionBy = ticketParam.bySystem?0:ticketParam.CurrLoginUserId,
                TicketId = this.ticket.Id,
            };

            dataProcessingProvider.createTicketActivity(ticketActivity);
            this.ticketActivity = ticketActivity;

            onActivityHandler(ticketActivity, ticketParam);
            
            //Save upload file

            if (ticketParam.FileUploads != null)
            {
                dataProcessingProvider.saveFileUploads(ticketParam.FileUploads, ticketActivity);
            }
            
        }

        public void setNotifyHandler(ITicketNotifyHandler notifyHandler)
        {
            this.notifyHandler = notifyHandler;
        }

        protected virtual void preProcessing()
        {
            //to be implimented 
        }
        protected virtual void postProcessing()
        {
            //to be implimented
        }
        protected abstract string getActivityType();

        protected abstract Dto.ActivityConfig getActivityConfig();

        protected bool isLoginAgent()
        {
            return currAgent != null ;
        }

        protected TicketAgent getCurrLoginAgent()
        {
            return currAgent;
        }
        
        private void lastUpdate()
        {
            setLastActivity();
            dataProcessingProvider.saveTicket(ticket);
        }

        protected virtual bool isUpladLastActivity()
        {
            return true;
        }

        private void setLastActivity()
        {
            ticket.LastAction = activityConfig.ActivityName;
            ticket.LastActionBy = ticketParam.CurrLoginUserId;
            ticket.LastActionDate = DateTime.Now;
            markFirstResponse();

        }


        protected void markFirstResponse()
        {
            if (!ticket.FirstResponse && ticket.PriorityId > 0 && activityConfig.FirstResponseMarked)
            {
                TicketSLA sla = dataProcessingProvider.getSLAByPriority(ticket.PriorityId);
                var firstResponseDate = ticket.CreatedDate.AddSeconds(sla.FirstResponsePeriod);
                if (firstResponseDate.CompareTo(DateTime.Now) >= 0)
                {
                    ticket.FirstResponse = true;
                }
            }
        }

        protected virtual void loadInfomation()
        {
            if (ticket != null)
            {

                this.submitter = dataProcessingProvider.getEmployee(ticket.SubmitterId);
                //None requestor
                if (ticket.RequestorId == 0)
                {
                    this.requestor = this.submitter;
                }
                //Unknow requestor
                else if (ticket.RequestorId==-1)
                {
                    this.requestor = dataProcessingProvider.getUnknowRequestor(ticket);
                }
                else
                {
                    this.requestor = dataProcessingProvider.getEmployee(ticket.RequestorId);
                }

                ticketDept = dataProcessingProvider.getDept(ticket.DeptId);
            }

            this.currLogin = dataProcessingProvider.getEmployee(ticketParam.CurrLoginUserId);

        }

        /***Assignment information**/
        private List<Employee> teamMembers = null;
        protected List<Employee> getTeamMembers()
        {

            if(teamMembers == null)
            {
                teamMembers = dataProcessingProvider.getTeamMember(ticket.LastAssTeamId);
            }
            return teamMembers;
        }

        private Employee assignedEmp = null;
        protected Employee getAssignedEmp()
        {
            if (assignedEmp == null && ticket.LastAssEmpId >0)
            {
                assignedEmp = dataProcessingProvider.getEmployee(ticket.LastAssEmpId);
            }

            return assignedEmp;
        }

        private List<AgentInfo> agentInfo = null;
        protected List<AgentInfo> getAgentInfoByTeam()
        {
            if(agentInfo == null)
            {
                agentInfo = dataProcessingProvider.getAgentInfoByTeam(ticket.LastAssTeamId);
            }

            return agentInfo;
        }

        private Employee currAssignedEmp;
        protected Employee getCurrAssignedEmp()
        {
            if(currAssignedEmp == null && ticket.LastAssEmpId > 0)
            {
                currAssignedEmp = dataProcessingProvider.getEmployee(ticket.LastAssEmpId);
            }
            return currAssignedEmp;
        }
        
        /****************************Notification*********************************/
        
        private void notify()
        {
            if(this.notifyHandler == null)
            {
                return;
            }

            this.notifyHandler.notify(getNotifyData());
        }


        public virtual AbstractNotifyData getNotifyData()
        {
            var notifyData = new DefaultNotifyData();

            notifyData.Activity = this.ticketActivity;

            var destUsers = getDestUsers();
            if ((activityConfig.NotifyType == NOTIFICATION_TYPE.EMAIL || NOTIFICATION_TYPE.EMAIL_UI == activityConfig.NotifyType) && destUsers != null && destUsers.Exists(t => t.DestType == DestUser.DEST_TYPE.TO))
            {
                var message = new NotifyMessage()
                {
                    Subject = getSubject(),
                    Body = getBody()

                };

                var notificationData = new DefaultNotifyData()
                {
                    DestUsers = getDestUsers(),
                    ticketDept = this.ticketDept,
                    ActionBy = this.currLogin,
                    Activity = this.ticketActivity,
                    IsMail = activityConfig.NotifyType == NOTIFICATION_TYPE.EMAIL || activityConfig.NotifyType == NOTIFICATION_TYPE.ALL || NOTIFICATION_TYPE.EMAIL_UI == activityConfig.NotifyType,
                    IsSMS = activityConfig.NotifyType == NOTIFICATION_TYPE.SMS || activityConfig.NotifyType == NOTIFICATION_TYPE.ALL,
                    IsUI = NOTIFICATION_TYPE.UI == activityConfig.NotifyType || activityConfig.NotifyType == NOTIFICATION_TYPE.ALL || NOTIFICATION_TYPE.EMAIL_UI == activityConfig.NotifyType,
                    NotifyMessage = message
                };

                return notificationData;

            }
            else
            {
                return null;
            }
        }

        private List<DestUser> destUsers = null;

        protected virtual List<DestUser> getDestUsers()
        {
            if( destUsers != null)
            {
                return destUsers;
            }
            destUsers = new List<DestUser>();
            destUsers.Add(new DestUser()
            {
                User = requestor,
                DestType = DestUser.DEST_TYPE.TO
            });

            if (!destUsers.Exists(t => t.User.Id == submitter.Id))
            {
                destUsers.Add(new DestUser()
                {
                    User = submitter,
                    DestType = DestUser.DEST_TYPE.CC
                });
            }
            
           
            return destUsers;
        }

        protected virtual string getSubject()
        {
            return string.Format("Naga1/GIT/Ticket Notification #{2} (Action: {0}, Subject: {1}) ", activityConfig.Action, ticket.Subject, ticket.TicketNo);
        }


        protected virtual string getBodyHeader()
        {
            return "This is notificaton email from ticket system. Please check more information:";
        }


        protected virtual string getBody()
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

        protected string getMailTo()
        {
            string recepient = string.Empty;
            var destUser = getDestUsers().FindAll(t => t.DestType == DestUser.DEST_TYPE.TO);
            foreach (var dest in destUser)
            {
                if (string.IsNullOrWhiteSpace(recepient))
                {
                    recepient = dest.User.DisplayName;
                }
                else
                {
                    recepient = recepient + " ," + dest.User.DisplayName;
                }
            }

            return recepient;
        }

        protected virtual string getSinature()
        {
            return "Ticket Automation Email";
        }
    }
}
