/**
*@author : Phanny
*/
using Workflow.DataObject.Ticket;

namespace Workflow.Business.Ticketing.Impl.DataFill
{
    public abstract class AbstractActivityDataFill
    {
        protected readonly IDataProcessingProvider dataProcessingProvider;
        public AbstractActivityDataFill(IDataProcessingProvider dataProcessingProvider)
        {
            this.dataProcessingProvider = dataProcessingProvider;
        }


        protected NotifyActivityDataDto getActivity(int activityId)
        {
            return dataProcessingProvider.getActivity(activityId);
        }
        
    }
}
