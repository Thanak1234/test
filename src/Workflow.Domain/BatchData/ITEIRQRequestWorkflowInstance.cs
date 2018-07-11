
using System.Collections.Generic;
using Workflow.Domain.Entities.ITEIRQ;
/**
*@author : Yim Samaune
*/

namespace Workflow.Domain.Entities.BatchData
{
    public class ITEIRQRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public EventInternet EventInternet { get; set; }

        public IEnumerable<Quotation> Quotations { get; set; }
        public IEnumerable<Quotation> DelQuotations { get; set; }
        public IEnumerable<Quotation> EditQuotations { get; set; }
        public IEnumerable<Quotation> AddQuotations { get; set; }
        
    }
}