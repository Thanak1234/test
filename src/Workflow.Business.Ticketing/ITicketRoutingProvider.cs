/**
*@author : Phanny
*/
using System.Collections.Generic;
using Workflow.Business.Ticketing.Dto;

namespace Workflow.Business.Ticketing
{
    public interface ITicketRoutingProvider
    {
        List<RoutingDestinationData> getRoutingDestination();
    }
}
