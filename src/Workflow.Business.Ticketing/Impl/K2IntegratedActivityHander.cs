/**
*@author : Phanny
*/
using Workflow.Business.Ticketing.Dto;

namespace Workflow.Business.Ticketing.Impl
{
    public class K2IntegratedActivityHander : AbstractTicketActivityHandler<SimpleActParams>, ITicketActivityHandler
    {
        public const string ACTIVITY_CODE = "K2_INTEGRATION";

        public K2IntegratedActivityHander(IDataProcessingProvider dataProcessingProvider, SimpleActParams ticketParam) : base(dataProcessingProvider, ticketParam) { }

        protected override ActivityConfig getActivityConfig()
        {
            return new ActivityConfig()
            {
                ActivityName = "K2 Integration",
                ActivityCode = ACTIVITY_CODE,
                Action = "Close Form",
                HasComment = REQURIED.REQUIRED
            };
        }

        protected override string getActivityType()
        {
            return ACTIVITY_CODE;
        }
    }
}
