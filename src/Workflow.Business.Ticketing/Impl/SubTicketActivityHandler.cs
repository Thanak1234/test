/**
*@author : Phanny
*/
using Workflow.Business.Ticketing.Dto;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.Business.Ticketing.Impl
{
    public class SubTicketActivityHandler : AbstractTicketActivityHandler<TicketParams>, ITicketActivityHandler
    {
        public const string ACTIVITY_CODE = "SUB_TICKET_POSTING";

      
        public SubTicketActivityHandler(IDataProcessingProvider dataProcessingProvider, TicketParams ticketParam) : base(dataProcessingProvider, ticketParam)
        {
        }

        protected override ActivityConfig getActivityConfig()
        {
            return new ActivityConfig()
            {
                ActivityName = "Sub Ticket",
                NotifyType = NOTIFICATION_TYPE.NONE,
                ActivityCode = ACTIVITY_CODE,
                Action = getAction(),
                HasComment = REQURIED.REQUIRED,
                TeamMemberAvailibility = true,
                UpdateLastAction = true,
                FirstResponseMarked = firstActionMarked()
            };
        }

        private bool firstActionMarked()
        {
            return ticketParam.StatusId > 1;
        }

        protected override string getActivityType()
        {
            return ACTIVITY_CODE;
        }

        private string getAction()
        {
            return "Post Subticket";
        }

        protected override bool isUpladLastActivity()
        {
            return true;
        }


        protected override void preProcessing()
        {
            ticketParam.ActComment = " Creating new sub ticket.";
        }

        /**
        * - Save subject ticket posting activity on main ticket
        * - Create new sub ticket creation
        */
        protected override void postProcessing()
        {

            var subTicketMessageHandler = new SubTicketActivityMessageHandler();

            //set ticket id to 0 to create ticket.
            ticketParam.TicketId = 0;
            var ticketActivityHandler = new TicketActivityHandler(dataProcessingProvider, ticketParam);
            ticketActivityHandler.addActMsgHandler(subTicketMessageHandler);
            ticketActivityHandler.takeAction();
            onTicketHandler(subTicketMessageHandler.getTicket());

            var subTkLink = new TicketSubTkLink()
            {
                ActivityId = ticketActivity.Id,
                SubTicketActId = subTicketMessageHandler.getTicketActivity().Id

            };

            dataProcessingProvider.saveSubTicketLink(subTkLink);
        }

    }


    class SubTicketActivityMessageHandler : IActivityMessageHandler
    {

        private Ticket ticket;
        private TicketActivity ticketActivity;
        public void onActivityCreation(TicketActivity activity, AbstractTicketParam tkParam)
        {
            if(!"TICKET_POSTING".Equals(activity.ActivityType) )
            {
                return;
            }

            this.ticketActivity = activity;
        }

        public void onTicketCreation(Ticket ticket)
        {

            this.ticket = ticket;
        }


        public Ticket getTicket()
        {
            return this.ticket;
        }


        public TicketActivity getTicketActivity()
        {
            return this.ticketActivity;
        }
    }
}
