/**
*@author : Phanny
*/
using System;
using Workflow.Business.Ticketing.Dto;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.Business.Ticketing.Impl
{
    public class TicketMergedActivityHandler : AbstractTicketActivityHandler<MergedTicketActParam>, ITicketActivityHandler
    {

        public const string ACTIVITY_CODE = "MERGE_TICKET";

        public TicketMergedActivityHandler(IDataProcessingProvider dataProcessingProvider, MergedTicketActParam ticketParam) : base(dataProcessingProvider, ticketParam) { }

        protected override ActivityConfig getActivityConfig()
        {
            return new ActivityConfig()
            {
                ActivityName = "Merged Ticket",
                ActivityCode = ACTIVITY_CODE,
                Action = "Merged Ticket ",
                HasComment = REQURIED.OPTIONAL
            };
        }

        protected override string getActivityType()
        {
            return ACTIVITY_CODE;
        }

        protected override void preProcessing()
        {
            ticket.StatusId = 8;
        }

        protected override void postProcessing()
        {
            var toTicketAct = new TicketActivity()
            {
                TicketId = ticketParam.MergedToTkId,
                Action = getActivityConfig().Action,
                ActivityType = getActivityType(),
                ActionBy = ticketParam.CurrLoginUserId,
                Description = ticketParam.ActComment
            };

            dataProcessingProvider.createTicketActivity(toTicketAct);


            var merged = new TicketMerged()
            {
                ActivityId = ticketActivity.Id,
                ToActivityId = toTicketAct.Id,
                ticketId = ticket.Id,
                ToTicketId = ticketParam.MergedToTkId
            };

            dataProcessingProvider.saveMergedTicket(merged);
            ticket.completedDate = DateTime.Now;
        }
    }
}
