/**
*@author : Phanny
*/
using System.Collections.Generic;
using Workflow.Domain.Entities.PBF;

namespace Workflow.Domain.Entities.BatchData
{
    public class PBFRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public ProjectBrief ProjectBrief { get; set; }

        public IEnumerable<Specification> Specifications { get; set; }
        public IEnumerable<Specification> DelSpecifications { get; set; }
        public IEnumerable<Specification> EditSpecifications { get; set; }
        public IEnumerable<Specification> AddSpecifications { get; set; }
    }
}