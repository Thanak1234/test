using System.Collections.Generic;
using Workflow.Domain.Entities.Admsr;

namespace Workflow.Domain.Entities.BatchData
{
    public class AdmsrRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public AdmsrInformation Information { get; set; }
        public List<AdmsrCompany> Companies { get; set; }
        public List<AdmsrCompany> NewCompanies { get; set; }
        public List<AdmsrCompany> ModifiedCompanies { get; set; }
        public List<AdmsrCompany> DeletedCompanies { get; set; }
    }
}
