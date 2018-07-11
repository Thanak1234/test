/**
*@author : Phanny
*/
using System.Collections.Generic;
using Workflow.Business.Ticketing.Dto;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.Business.Ticketing.Impl
{
    public class PostReplyActivityHandler : AbstractTicketActivityHandler<PostReplyTicketParams>, ITicketActivityHandler
    {

        public const string ACTIVITY_CODE = "POST_REPLY";
   
        private enum POST_REPLY
        {
            TO_USER,
            TO_AGENT
        };


        private POST_REPLY replyTo;

        public PostReplyActivityHandler(IDataProcessingProvider dataProcessingProvider, PostReplyTicketParams ticketParam) : base(dataProcessingProvider, ticketParam) { }


        protected override void preProcessing()
        {
            if(replyTo == POST_REPLY.TO_USER)
            {
                var currAssingedEmpId = getCurrAssignedEmp() == null ? 0 : getCurrAssignedEmp().Id;
                if (currAssingedEmpId == 0 || currAssingedEmpId != ticketParam.CurrLoginUserId) //assigned to current login agent 
                {
                    autoAssigned(getCurrLoginAgent());
                }

            }

            if (replyTo == POST_REPLY.TO_AGENT && WAIT_ACTIONED_BY_USER.Equals(ticket.WaitActionedBy))
            {
                ticket.WaitActionedBy = WAIT_ACTIONED_BY_AGENT;
            }else if (replyTo == POST_REPLY.TO_USER && WAIT_ACTIONED_BY_AGENT.Equals(ticket.WaitActionedBy))
            {
                ticket.WaitActionedBy = WAIT_ACTIONED_BY_USER;
            }
        }



        private void autoAssigned(TicketAgent tAgent)
        {
            //Auto assign
            AssignedTicketParams assignedTicketParams = new AssignedTicketParams()
            {
                TicketId = ticketParam.TicketId,
                ActivityCode = AssignTicketActivityHandler.ACTIVITY_CODE,
                ActionCode ="Auto Assigned",
                TeamId= ticket.LastAssTeamId,
                Assignee = tAgent.Id,
                ActComment = "Auto assigned since agent take [post reply] action.",
                bySystem = true

            };
            ITicketActivityHandler assinged = new AssignTicketActivityHandler(ACTIVITY_CODE,ticket, dataProcessingProvider, assignedTicketParams, actMssgHandlers);
            assinged.takeAction();
        }
    

        protected override Dto.ActivityConfig getActivityConfig()
        {
           return  new Dto.ActivityConfig()
            {
                ActivityName = "Post Reply",
                ActivityCode = ACTIVITY_CODE,
                Action = getAction(),
                HasComment = REQURIED.REQUIRED,
                NotifyType =NOTIFICATION_TYPE.EMAIL_UI
           };
        }

        protected override string getActivityType()
        {
            return ACTIVITY_CODE;
        }


        private string _action;
        private string getAction()
        {

            if (_action != null) return _action;

            var isOriginator = (ticket.RequestorId == ticketParam.CurrLoginUserId || ticket.SubmitterId == ticketParam.CurrLoginUserId);

            var currAssingedEmpId = getCurrAssignedEmp() == null ? 0 : getCurrAssignedEmp().Id;
            if (isOriginator && isLoginAgent() && (currAssingedEmpId == 0 || currAssingedEmpId == ticketParam.CurrLoginUserId))
            {
                isOriginator = WAIT_ACTIONED_BY_USER.Equals(ticket.WaitActionedBy) ;
            }

            if (isOriginator)
            {
                replyTo = POST_REPLY.TO_AGENT;
            }
            else
            {
                replyTo = POST_REPLY.TO_USER;
            }

            _action= replyTo == POST_REPLY.TO_USER ?"Replied to User" : "Replied To Agent";
            return _action;
        }

        private List<DestUser> destUsers = null;
        protected override List<DestUser> getDestUsers()
        {
            if (destUsers != null)
            {
                return destUsers;
            }

            destUsers = new List<DestUser>();

            if (replyTo == POST_REPLY.TO_USER)
            {
                
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
                
            }
            else
            {
                if (ticket.LastAssEmpId > 0)
                {
                    destUsers.Add(new DestUser()
                    {
                        User = getCurrAssignedEmp(),
                        DestType = DestUser.DEST_TYPE.TO
                    });
                }
            }

            return destUsers;
        }

        //private List<String> to;
        //protected override List<string> messgeTo()
        //{

        //    if (to != null) return to;

        //    var empList = new List<Employee>();
        //    if (replyTo == POST_REPLY.TO_USER)
        //    {
        //        //to.Add(getRequestor().Email);
        //        empList.Add(getRequestor());
        //        if (getRequestor().Id != getSubmittor().Id)
        //        {
        //            //to.Add(getSubmittor().Email);
        //            empList.Add(getSubmittor());
        //        }
        //    }
        //    else
        //    {
        //        if (ticket.LastAssEmpId > 0)
        //        {
        //            var employee = dataProcessingProvider.getEmployee(ticket.LastAssEmpId);
        //            //to.Add(employee.Email);
        //            empList.Add(employee);
        //        }
        //    }
        //   to = new List<string>();

        //    empList.ForEach(t => {
        //        to.Add(t.Email);

        //        if (string.IsNullOrEmpty(toByName)){
        //            toByName = t.DisplayName;
        //        }
        //        else
        //        {
        //            toByName += ", " + t.DisplayName;
        //        }

        //    });


        //    return to;
        //}

        //protected override List<string> messageCc()
        //{
        //    var cc = new List<string>();
        //    return cc;
        //}

        protected override string getBodyHeader()
        {
            return "You have been replied with the information below:";
        }

       

    }
}
