/**
*@author : Phanny
*/
using Workflow.Business.Ticketing.Dto;

namespace Workflow.Business.Ticketing.Impl
{
    public class PostPublicNoteActivityHandler : AbstractTicketActivityHandler<SimpleActParams>, ITicketActivityHandler
    {

        public const string ACTIVITY_CODE = "POST_PUBLIC_NOTE";

        public PostPublicNoteActivityHandler(IDataProcessingProvider dataProcessingProvider, SimpleActParams ticketParam) : base(dataProcessingProvider, ticketParam) { }

        protected override ActivityConfig getActivityConfig()
        {
            return new ActivityConfig()
            {
                ActivityName = "Post Public Note",
                ActivityCode = ACTIVITY_CODE,
                Action = "Post Note",
                HasComment = REQURIED.REQUIRED
            };
        }

        protected override string getActivityType()
        {
            return ACTIVITY_CODE;
        }
    }
}
