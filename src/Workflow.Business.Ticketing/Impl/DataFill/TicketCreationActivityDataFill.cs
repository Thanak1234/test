/**
*@author : Phanny
*/
namespace Workflow.Business.Ticketing.Impl.DataFill
{
    public class TicketCreationActivityDataFill : AbstractActivityDataFill, IActivityDataFill
    {

        public TicketCreationActivityDataFill(IDataProcessingProvider dataProcessingProvider):base(dataProcessingProvider) { }

        public object getData(int activityId)
        {
            var activity = getActivity(activityId);
            var description = "Your ticket <b> #{0} </b> have been created via Email. <br> subject: {1}  ";
            return new {
                ticketId = activity.ticketId,
                ticketNo = activity.ticketNo,
                subject = activity.subject,
                description = string.Format(description, activity.ticketNo, activity.subject)
            };
        }
    }
}
