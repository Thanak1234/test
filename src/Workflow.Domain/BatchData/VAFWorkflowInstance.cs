using System.Collections.Generic;
using Workflow.Domain.Entities.VAF;

namespace Workflow.Domain.Entities.BatchData
{
    public class VAFWorkflowInstance : AbstractWorkflowInstance
    {
        public Information Information { get; set; }
        public List<Outline> AllOutlines { get; set; }
        public List<Outline> NewOutlines { get; set; }
        public List<Outline> ModifiedOutlines { get; set; }
        public List<Outline> DeletedOutlines { get; set; }
    }
}