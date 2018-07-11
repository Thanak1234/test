/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.Reservation;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.Domain.Entities.BatchData
{
    public class FnFRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public Booking Reservation { get; set; }

        public IEnumerable<OccupancyDto> Occupancies { get; set; }
        public IEnumerable<OccupancyDto> DelOccupancies { get; set; }
        public IEnumerable<OccupancyDto> EditOccupancies { get; set; }
        public IEnumerable<OccupancyDto> AddOccupancies { get; set; }
    }
}
