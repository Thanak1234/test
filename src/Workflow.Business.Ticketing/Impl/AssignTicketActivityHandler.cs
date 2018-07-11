/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using Workflow.Business.Ticketing.Dto;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.Business.Ticketing.Impl
{
    public class AssignTicketActivityHandler : AbstractTicketActivityHandler<AssignedTicketParams>, ITicketActivityHandler
    {

        public const string ACTIVITY_CODE = "TICKET_ASSIGNED";
        private string flag;
        private bool isUpladLastAct = true;
        private bool isSubProcess = false;

        public AssignTicketActivityHandler(IDataProcessingProvider dataProcessingProvider, AssignedTicketParams ticketParam) : base(dataProcessingProvider, ticketParam){}
        public AssignTicketActivityHandler(string flag, Ticket ticket, IDataProcessingProvider dataProcessingProvider, AssignedTicketParams ticketParam, List<IActivityMessageHandler> actMssgHandlers) : base(dataProcessingProvider, ticketParam)
        {
            this.flag = flag;
            isUpladLastAct = TicketActivityHandler.ACTIVITY_CODE.Equals(flag)|| false  ;
            this.isSubProcess = ticket != null;
            base.ticket = ticket;
            base.actMssgHandlers = actMssgHandlers;
        }

        /**
        Update all existing assigment to expiry
        */
        protected override void preProcessing()
        {

            if (!isLoginAgent() && !ticketParam.bySystem && !this.isSubProcess)
            {
                throw new Exception("User cannot make assignment");
            }

            //Validation
            if (ticketParam.TeamId == 0)
            {
                return;
            }


            int empId = 0;

            if (ticketParam.Assignee > 0)
            {
                var assignedAgent = dataProcessingProvider.getAgentById(ticketParam.Assignee);
                empId = assignedAgent.EmpId;
            }

            
            if(ticketParam.TeamId == ticket.LastAssTeamId && empId == ticket.LastAssEmpId)
            {
                throw new Exception(" Please a differenct assignment.");
            }


            dataProcessingProvider.expiredCurrAssigned(ticket);
        }

        protected override void postProcessing()
        {

            bool defaultTeam = false, defaultAgent = false;
            string assignDesc ="";

            if (ticketParam.TeamId == 0)
            {
                var ticketItem = dataProcessingProvider.getTicketItem(ticket.TicketItemId);
                if(ticketItem==null && ticketItem.TeamId == 0)
                {
                    throw new Exception("Default team cannot be found");
                }
                ticketParam.TeamId = ticketItem.TeamId;
                defaultTeam = true;
            }


            if (ticketParam.Assignee == 0)
            {
                int defaultAssignee = dataProcessingProvider.getDefaultAgentByTeamId(ticketParam.TeamId);
                if (defaultAssignee > 0)
                {
                    ticketParam.Assignee = defaultAssignee;
                    defaultAgent = true;
                }                
            }

            if (defaultTeam)
            {
                assignDesc = "Default team is selected. ";
            }

            if (defaultAgent)
            {
                assignDesc += "Default agent is selected";
            }

            TicketAssignment ticketAssignment = new TicketAssignment()
            {
                TicketActivityId = ticketActivity.Id,
                TeamId = ticketParam.TeamId,
                AssigneeId = ticketParam.Assignee,
            };

            if (!String.IsNullOrEmpty(assignDesc))
            {
                ticketAssignment.Description= assignDesc;
            }

            dataProcessingProvider.saveTicketAssignment(ticketAssignment);

            //bool updateTicketRequired = false;

            if (!WAIT_ACTIONED_BY_AGENT.Equals(ticket.WaitActionedBy)  && isUpladLastActivity() )
            {
                ticket.WaitActionedBy = WAIT_ACTIONED_BY_AGENT;
                //updateTicketRequired = true;
            }

            if (ticketParam.TeamId > 0  && ticketParam.TeamId!=ticket.LastAssTeamId )
            {
                ticket.LastAssTeamId = ticketParam.TeamId;
                //updateTicketRequired = true;
            }

            var assingedEmpId = 0;
            if (ticketParam.Assignee != 0)
            {
                var assignedAgent = dataProcessingProvider.getAgentById(ticketParam.Assignee);
                assingedEmpId =  assignedAgent.EmpId;
            }
           
            if (assingedEmpId != ticket.LastAssTeamId)
            {
                ticket.LastAssEmpId = assingedEmpId;
                //updateTicketRequired = true;
            }
            
        }
        
        protected override Dto.ActivityConfig getActivityConfig()
        {
            return new Dto.ActivityConfig()
            {
                ActivityName = "Assign Ticket",
                ActivityCode = ACTIVITY_CODE,
                Action = "Assigned",
                HasComment = REQURIED.OPTIONAL,
                NotifyType = NOTIFICATION_TYPE.EMAIL_UI
            };
        }
        
        protected override string getActivityType()
        {
            return ACTIVITY_CODE;
        }

        protected override bool isUpladLastActivity()
        {
            return this.isUpladLastAct;
        }


        private List<DestUser> destUsers = null;
        protected override List<DestUser> getDestUsers()
        {
            if(destUsers != null)
            {
                return destUsers;
            }
           
            destUsers = new List<DestUser>();

            if (ticket.LastAssEmpId != 0)
            {

                if (getCurrAssignedEmp() != null)
                {
                    destUsers.Add(new DestUser() {
                        DestType = DestUser.DEST_TYPE.TO,
                        User= getCurrAssignedEmp()
                    });
                }
            }
            else
            {
                return destUsers;
            }


            var list = getAgentInfoByTeam();
            if (list != null)
            {
                list.ForEach(t => {
                    if (TicketGroupPolicy.TEAM_TICKET.Equals(t.AssignedNotify)  && !destUsers.Exists(des=>des.User.Id == t.EmpId))
                    {
                        destUsers.Add(new DestUser()
                        {
                            DestType = DestUser.DEST_TYPE.CC,
                            User = new Employee()
                            {
                                Id = t.EmpId,
                                Email= t.Email,
                                DisplayName = t.EmpName,
                                EmpNo = t.EmpNo
                            }
                        });
                    }
                });
            }

            return destUsers;
        }
        protected override string getBodyHeader()
        {
            return "You have been assigned to ticket with the information below:";
        }


    }
}
