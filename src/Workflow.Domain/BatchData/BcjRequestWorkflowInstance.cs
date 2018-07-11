using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.BCJ;

namespace Workflow.Domain.Entities.BatchData
{
    public class BcjRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public ProjectDetail ProjectDetail { get; set; }

        public IEnumerable<BcjRequestItem> RequestItems { get; set; }
        public IEnumerable<BcjRequestItem> DelRequestItems { get; set; }
        public IEnumerable<BcjRequestItem> EditRequestItems { get; set; }
        public IEnumerable<BcjRequestItem> AddRequestItems { get; set; }

        public IEnumerable<AnalysisItem> AnalysisItems { get; set; }
        public IEnumerable<AnalysisItem> DelAnalysisItems { get; set; }
        public IEnumerable<AnalysisItem> EditAnalysisItems { get; set; }
        public IEnumerable<AnalysisItem> AddAnalysisItems { get; set; }

        public IEnumerable<PurchaseOrder> PurchaseOrderItems { get; set; }
        public IEnumerable<PurchaseOrder> DelPurchaseOrderItems { get; set; }
        public IEnumerable<PurchaseOrder> EditPurchaseOrderItems { get; set; }
        public IEnumerable<PurchaseOrder> AddPurchaseOrderItems { get; set; }

    }
}
