/**
*@author : Phanny
*/
using System;

namespace Workflow.Business.Ticketing.Impl.DataFill
{
    public class TicketStatusActivityDataFill : AbstractActivityDataFill, IActivityDataFill
    {

        public TicketStatusActivityDataFill(IDataProcessingProvider dataProcessingProvider) : base(dataProcessingProvider) { }
        public object getData(int activityId)
        {

            try
            {
                var activity = getActivity(activityId);

                var description = "Ticket <b>#{0}</b> has closed by {1} ";
                var data = new
                {
                    ticketId = activity.ticketId,
                    ticketNo = activity.ticketNo,
                    action = activity.action,
                    actionBy = activity.actionBy,
                    description = string.Format(description, activity.ticketNo, activity.actionBy)
                };

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
