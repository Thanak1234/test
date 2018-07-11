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
    public class TicketActivityHandler : AbstractTicketActivityHandler<TicketParams>, ITicketActivityHandler
    {

        public const string ACTIVITY_CODE = "TICKET_POSTING";
        private bool reassigned= false;
        private bool statusChanged= false;
        public TicketActivityHandler(IDataProcessingProvider dataProcessingProvider, TicketParams ticketParam) : base(dataProcessingProvider, ticketParam)
        {
        }

        protected override bool isUpladLastActivity()
        {
            return false;
        }

        protected override string getActivityType()
        {
            return ACTIVITY_CODE;
        }

        protected override ActivityConfig getActivityConfig()
        {
            return new ActivityConfig()
            {
                ActivityName = "Ticket",
                NotifyType = isNotify(),
                ActivityCode = ACTIVITY_CODE,
                Action = getAction(),
                HasComment = REQURIED.NONE,
                TeamMemberAvailibility = true,
                UpdateLastAction = false,
                FirstResponseMarked = firstActionMarked()
            };
        }

        private bool firstActionMarked()
        {
            return ticketParam.StatusId > 1;
        }

        private NOTIFICATION_TYPE isNotify()
        {
            if(!isEdit())
            {
                if (TicketParams.INTEGRATED_TYPE.EMAIL ==  ticketParam.AutomationType)
                {
                    return NOTIFICATION_TYPE.EMAIL_UI;
                }
                else if(TicketParams.INTEGRATED_TYPE.NONE == ticketParam.AutomationType)
                {
                    return NOTIFICATION_TYPE.EMAIL;
                }
                
            }
            
            return NOTIFICATION_TYPE.NONE;
            
        }

        protected override void validate()
        {
            if (string.IsNullOrWhiteSpace(ticketParam.Description))
            {
                throw new Exception("Description is required.");
            }


            if (string.IsNullOrWhiteSpace(ticketParam.Subject))
            {
                throw new Exception("Subject is required.");
            }
            var assignEmpId = 0;
            if (ticketParam.Assignee > 0)
            {
                var assignedAgent = dataProcessingProvider.getAgentById(ticketParam.Assignee);
                assignEmpId = assignedAgent.EmpId;
            }

            if (ticket != null)
            {
                reassigned =  ticket.LastAssEmpId != assignEmpId || ticket.LastAssTeamId != ticketParam.TeamId;
                statusChanged = ticket.StatusId != ticketParam.StatusId;
            }


            if (isEdit() && string.IsNullOrWhiteSpace(ticketParam.ActComment))
            {
                if(statusChanged)
                {
                    throw new Exception("Comment is required since status has been changed");
                }
                //else if(reassigned)
                //{
                //    throw new Exception("Comment is required  since assignment has been changed");
                //}
            }
        }

        protected override void preProcessing()
        {
            saveTicket();
            onTicketHandler(ticket);
        }

        private void saveTicket()
        {

            TicketSLA sla = null;

            //SLA = mapping between ticket type and priority, Veasna-20170213
            if (ticketParam.PriorityId > 0 && ticketParam.TicketTypeId > 0)
            {               
                sla = dataProcessingProvider.getSLAByPriorityAndTicketType(ticketParam.PriorityId, ticketParam.TicketTypeId);
            }

            if (ticket != null)
            {

                //Prevent set requestor to none emp(0) if requestor from unknow employee
                if(ticket.RequestorId == -1 && ticketParam.RequestorId ==0)
                {
                    ticketParam.RequestorId = -1;
                }


                //Prevent set requestor to none emp(0) if submitter is system account(K2 service ..etc)
                if ("GENERIC".Equals(requestor.EmpType) && ticketParam.RequestorId == 0)
                {
                    ticketParam.RequestorId = ticket.RequestorId;
                }

                if (sla!= null && ticket.CreatedDate !=null && ticket.PriorityId != ticketParam.PriorityId && ticketParam.DueDate == null)
                {
                    ticketParam.DueDate = getDueDate(sla, ticket.CreatedDate);
                }

                ticket = dataProcessingProvider.getTicket(ticketParam.TicketId);
                ticket.TicketTypeId = ticketParam.TicketTypeId;
                ticket.SiteId = ticketParam.SiteId;
                //No need to update
                //ticket.SubmitterId = ticketParam.CurrLoginUserId;
                ticket.RequestorId = ticketParam.RequestorId;
                ticket.PriorityId = ticketParam.PriorityId;
                ticket.UrgentcyId = ticketParam.UrgencyId;
                ticket.ImpactId = ticketParam.ImpactId;
                ticket.SourceId = ticketParam.SourceId;
                
                //Ticket status will be handle by assign ticket activity task
                //if (ticketParam.StatusId > 0)
                //{
                //    ticket.StatusId = ticketParam.StatusId;
                //}
                
                ticket.TicketItemId = getTicketItemId();
                ticket.Subject = ticketParam.Subject;
                ticket.Description = ticketParam.Description;
                ticket.LastAction = getAction();
                ticket.DueDate = ticketParam.DueDate;
                ticket.EstimatedHours = ticketParam.EstimatedHours;
                ticket.LastActionBy = ticketParam.CurrLoginUserId;
                ticket.LastActionDate = DateTime.Now;
                ticket.SlaId = ticketParam.SlaId;
                //ticket.Reference = ticketParam.Reference;
                //ticket.RefType = ticketParam.RefType;
            }
            else
            {
                ticket = new Ticket()
                {
                    TicketNo = getNextTicketNo(),
                    TicketTypeId = ticketParam.TicketTypeId,
                    SiteId = ticketParam.SiteId,
                    SubmitterId = ticketParam.CurrLoginUserId,
                    RequestorId = ticketParam.RequestorId,

                    PriorityId = ticketParam.PriorityId,
                    UrgentcyId = ticketParam.UrgencyId,
                    ImpactId = ticketParam.ImpactId,

                    SourceId = ticketParam.SourceId,
                    StatusId = ticketParam.StatusId == 0 ? 1 : ticketParam.StatusId,

                    TicketItemId = getTicketItemId(),

                    DeptId = ticketParam.DeptOwnerId,

                    Subject = ticketParam.Subject,
                    Description = ticketParam.Description,

                    DueDate = ticketParam.DueDate,
                    EstimatedHours = ticketParam.EstimatedHours,

                    LastAction = getAction(),
                    LastActionBy = ticketParam.CurrLoginUserId,
                    LastActionDate = DateTime.Now,
                    SlaId = ticketParam.SlaId,
                    RefType = ticketParam.RefType,
                    Reference = ticketParam.Reference
                };

                var comment = "Post new ticket ";

                //Check for none regestered employee

                if (ticket.SourceId == 1)
                {
                    comment += " via E-Mail";
                }else if(ticket.SourceId == 3)
                {
                    comment += " via Web Form";
                }else if(ticket.SourceId ==5)
                {
                    comment += " via K2 Form Integrated";
                }
                ticketParam.ActComment = comment;


                if(sla != null && ticketParam.DueDate == null)
                {
                    ticket.DueDate = getDueDate(sla, ticket.CreatedDate);
                }

                if(ticket.StatusId > 1)
                {
                    base.markFirstResponse();
                }
                
            }

            dataProcessingProvider.saveTicket(ticket);
            ticketParam.FileUploads = ticketParam.UserAttachFiles;
            //Remove upload file
            //se.ticket = ticket;

            //Record originator information
            if(ticket.RequestorId == -1 && TicketParams.INTEGRATED_TYPE.EMAIL==ticketParam.AutomationType && ticketParam.TicketNoneReqEmp != null)
            {
                var ticketNoneReqEmp = new TicketNoneReqEmp()
                {
                    TicketId = ticket.Id,
                    Originator = ticketParam.TicketNoneReqEmp.Originator,
                    Receipient = ticketParam.TicketNoneReqEmp.Receipient,
                    Cc = ticketParam.TicketNoneReqEmp.Cc,
                    EmailItemId = ticketParam.TicketNoneReqEmp.EmailItemId
                };

                dataProcessingProvider.saveTicketNoneReqEmp(ticketNoneReqEmp);


                this.requestor = new Domain.Entities.Employee()
                {
                    Id = -1,
                    Email = ticketNoneReqEmp.Originator,
                    EmpNo = "Unknown",
                    DisplayName = ticketNoneReqEmp.Originator
                };
            }
        }


        protected override void postProcessing()
        {

            //No need to assigned
            if (isEdit() && statusChanged)
            {
                ChangeStatusActParams statusParams = new ChangeStatusActParams()
                {
                    TicketId = ticket.Id,
                    ActivityCode = ChangeStatusActivityHandler.ACTIVITY_CODE,
                    ActionCode = "Auto change",
                    ActComment = ticketParam.ActComment,
                    CurrLoginUserId = ticketParam.CurrLoginUserId,
                    StatusId = ticketParam.StatusId
                };
                
                ITicketActivityHandler statusAct = new ChangeStatusActivityHandler(ACTIVITY_CODE, ticket, dataProcessingProvider, statusParams, actMssgHandlers);
                statusAct.setNotifyHandler(notifyHandler);
                statusAct.takeAction();
            } 

            if(!isEdit() || reassigned)
            {
                AssignedTicketParams assignedTkParams = new AssignedTicketParams()
                {
                    TicketId = ticket.Id,
                    ActivityCode = AssignTicketActivityHandler.ACTIVITY_CODE,
                    ActionCode = "Auto Assigned",
                    ActComment = "Auto assigned",
                    TeamId = ticketParam.TeamId,
                    Assignee = ticketParam.Assignee,
                    CurrLoginUserId = ticketParam.CurrLoginUserId,
                    bySystem = TicketParams.INTEGRATED_TYPE.NONE !=ticketParam.AutomationType

                };
                ITicketActivityHandler assigned = new AssignTicketActivityHandler(ACTIVITY_CODE, ticket, dataProcessingProvider, assignedTkParams, actMssgHandlers);
                assigned.setNotifyHandler(notifyHandler);
                assigned.takeAction();
            }
            
        }

        /****/

        private string getNextTicketNo()
        {
            var lastTicketNo = dataProcessingProvider.getLastTicketNo();
            int lateNo = 0;
            if (lastTicketNo != null)
            {
                lateNo = int.Parse(lastTicketNo);
            }

            return String.Format("{0:D6}", lateNo + 1);
        }

        private int getTicketItemId()
        {
            if (ticketParam.TicketItemId > 0)
            {
                return ticketParam.TicketItemId;
            }

            if((ticketParam.DeptOwnerId == 0)){
                throw new Exception("Default department cannot be found for detault ticket item");
            }
           
            if (ticketParam.DeptOwnerId > 0)
            {
                return dataProcessingProvider.getDefaultTicketItem(ticketParam.DeptOwnerId);
            }

            throw new Exception("Ticket item cannot be found for ticket creation. If ticket itme is not selected, it's taking from defaul item of department setup. Please contact administrator");
        }

        private string getAction()
        {
            if (!isEdit())
            {
                return "Post New Ticket";
            }
            return "Edit Ticket";
        }

        private bool isEdit()
        {
            return ticketParam.TicketId > 0;
        }

        protected override string getBodyHeader()
        {
            return "Your ticket has been created. Please keep ticket number to keep track ticket activities:";
        }

        private List<DestUser> destUsers = null;
        protected override List<DestUser> getDestUsers()
        {
            if (destUsers != null)
            {
                return destUsers;
            }
            destUsers = new List<DestUser>();
            destUsers.Add(new DestUser()
            {
                User = requestor,
                DestType = DestUser.DEST_TYPE.TO
            });

            return destUsers;

        }

        protected override string getBody()
        {

            var html = @"Dear {{recepient}}, <br/><br/>
                        {{bodyHeader}}<br/>
                        <h3>Ticket Summary</h3>
                        <p><strong>Ticket No</strong> : # {{ticketNo}}</p>
                        <p><strong> Create Date </strong>: {{createdData}}</p>
                        <p><strong> Requestor </strong>: {{requestor}}({{empNo}})</p>
                        <p><strong> Subject  </strong>: {{subject}}</p>
                        <p><strong>Description </strong>:
                        <div style='line-height:1.4;border-left:2px solid #009688;margin-left:20'><div style ='margin-left:15'>" + ticket.Description + "</div></div> <br/><strong>{{signature}}</strong><br/><br/>";
            html += "<b><span style='font-size:10.0pt;font-family:&quot;ITC Stone Sans Std Medium&quot;; color:navy'>Internet E-mail Confidentiality Footer</span></b>  <p class='MsoNormal'><i><span style='font-family:&quot;ITC Stone Sans Std Medium&quot;;color:navy'>This E-mail is confidential and intended only for the use of the individual(s) or entity named above and may contain information that is privileged.&nbsp; If you are not the intended recipient, you are advised that any dissemination, distribution or copying of this E-mail is strictly prohibited. If you have received this E-mail in error, please notify us immediately by return E-mail or telephone and destroy the original message.</span></i><o:p></o:p></p>";

            var template = Handlebars.Compile(html);
            var data = new
            {
                recepient = getMailTo(),
                subject = ticket.Subject,
                bodyHeader = getBodyHeader(),
                ticketNo = ticket.TicketNo,
                requestor = requestor.DisplayName,
                empNo = requestor.EmpNo,
                action = getActivityConfig().Action,
                createdData = String.Format("{0:d/M/yyyy HH:mm:ss}", ticketActivity.CreatedDate),
                signature = getSinature()
            };

            html = template(data);
            return html;
        }


        private DateTime getDueDate(TicketSLA sla, DateTime createdDate)
        {

            return createdDate.AddSeconds(sla.GracePeriod);

        }

        protected override void loadInfomation()
        {

            if(ticket != null)
            {
                base.loadInfomation();
                return;
            }


            if (ticketParam.DeptOwnerId == 0)
            {
                try
                {
                    ticketParam.DeptOwnerId = dataProcessingProvider.getDefaultDepartmentId();
                  
                }
                catch 
                {
                    throw new Exception("Cannot find default department. Default department must have one in the whole system.");
                }

            }

            base.ticketDept = dataProcessingProvider.getDept(ticketParam.DeptOwnerId);

            base.submitter = dataProcessingProvider.getEmployee(ticketParam.CurrLoginUserId);
            //None requestor
            if (ticketParam.RequestorId == 0)
            {
                this.requestor = this.submitter;
            }
            else
            {
                base.requestor = dataProcessingProvider.getEmployee(ticketParam.RequestorId);
            }
        }

    }
               


}
