using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.HR;

namespace Workflow.Domain.Entities.BatchData
{
   public class ATTRequestWorkflowInstance:AbstractWorkflowInstance
    {
        public TravelDetail TravelDetail { get; set; }

        public IEnumerable<Destination> Destinations { get; set; }
        public IEnumerable<Destination> DelRequestDestinations { get; set; }
        public IEnumerable<Destination> EditRequestDestinations { get; set; }
        public IEnumerable<Destination> AddRequestDestinations { get; set; }

        public IEnumerable<FlightDetail> FlightDetails { get; set; }
        public IEnumerable<FlightDetail> DelRequestFlightDetails { get; set; }
        public IEnumerable<FlightDetail> EditRequestFlightDetails { get; set; }
        public IEnumerable<FlightDetail> AddRequestFlightDetails { get; set; }
    }
}
