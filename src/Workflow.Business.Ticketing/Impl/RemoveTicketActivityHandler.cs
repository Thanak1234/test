/**
*@author : Phanny
*/
using System;
using Workflow.Business.Ticketing.Dto;

namespace Workflow.Business.Ticketing.Impl
{
    public class RemoveTicketActivityHandler : AbstractTicketActivityHandler<SimpleActParams>, ITicketActivityHandler
    {

        public const string ACTIVITY_CODE = "DELETE_TICKET";

        public RemoveTicketActivityHandler(IDataProcessingProvider dataProcessingProvider, SimpleActParams ticketParam) : base(dataProcessingProvider, ticketParam) { }


        protected override void preProcessing()
        {
            ticket.StatusId = 7;
            ticket.completedDate = DateTime.Now;
            ticket.WaitActionedBy = null;
        }

        protected override ActivityConfig getActivityConfig()
        {
            return new ActivityConfig()
            {
                ActivityName = "Ticket Removal",
                ActivityCode = ACTIVITY_CODE,
                Action = "Remove Ticket",
                HasComment = REQURIED.REQUIRED
            };
        }

        protected override string getActivityType()
        {
            return ACTIVITY_CODE;
        }
    }
}
