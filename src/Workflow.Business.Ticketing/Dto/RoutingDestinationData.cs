using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.Ticketing.Dto
{
    public enum DestinationType
    {
        AVAILABLE,  //Can view ticket
        ASSIGNED, // have been assigned
        ALLOCATED, // 
        OPENED
    }

    public class RoutingDestinationData
    {
        public int EmpId { get; set; }
    }
}
