/**
*@author : Yim Samaune
*/
using System.Collections.Generic;
using System.Linq;
using Workflow.Domain.Entities.MTF;

namespace Workflow.Domain.Entities.BatchData
{
    public class MTFRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public Treatment Treatment { get; set; }

        public IEnumerable<Prescription> Prescriptions { get; set; }
        public IEnumerable<Prescription> DelPrescriptions { get; set; }
        public IEnumerable<Prescription> EditPrescriptions { get; set; }
        public IEnumerable<Prescription> AddPrescriptions { get; set; }

        public IEnumerable<UnfitToWork> UnfitToWorks { get; set; }
        public IEnumerable<UnfitToWork> DelUnfitToWorks { get; set; }
        public IEnumerable<UnfitToWork> EditUnfitToWorks { get; set; }
        public IEnumerable<UnfitToWork> AddUnfitToWorks { get; set; }
    }
}