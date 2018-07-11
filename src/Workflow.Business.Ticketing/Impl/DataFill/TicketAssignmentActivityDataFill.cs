/**
*@author : Phanny
*/
using System;

namespace Workflow.Business.Ticketing.Impl.DataFill
{
    public class TicketAssignmentActivityDataFill : AbstractActivityDataFill, IActivityDataFill
    {

        public TicketAssignmentActivityDataFill(IDataProcessingProvider dataProcessingProvider):base(dataProcessingProvider) { }

        public object getData(int activityId)
        {
            try
            {
                var activity = getActivity(activityId);
                var description = "You have been assigned to ticket <b>#{0}</b> by {1}";
                var actionBy = activity.actionBy == null ? "System" : activity.actionBy;

                var data = new
                {
                    ticketId = activity.ticketId,
                    ticketNo = activity.ticketNo,
                    team = activity.team,
                    assignee = activity.assignee,
                    actionBy = actionBy,
                    description = string.Format(description, activity.ticketNo, actionBy)
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
