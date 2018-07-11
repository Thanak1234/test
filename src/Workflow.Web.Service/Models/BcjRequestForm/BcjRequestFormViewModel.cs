using System.Collections.Generic;

namespace Workflow.Web.Models.BcjRequestForm
{
    public class BcjRequestFormViewModel : AbstractFormDataViewModel
    {
        private DataItem _dataItem;

        public DataItem dataItem
        {
            get
            {
                return _dataItem ?? (_dataItem = new DataItem());
            }
            set { _dataItem = value; }
        }
    }

    public class DataItem
    {

        public ProjectDetailViewModel projectDetail { get; set; }
        public IEnumerable<RequestItemViewModel> requestItems { get; set; }
        public IEnumerable<RequestItemViewModel> addRequestItems { get; set; }
        public IEnumerable<RequestItemViewModel> editRequestItems { get; set; }
        public IEnumerable<RequestItemViewModel> delRequestItems { get; set; }

        public IEnumerable<AnalysisItemViewModel> analysisItems { get; set; }
        public IEnumerable<AnalysisItemViewModel> addAnalysisItems { get; set; }
        public IEnumerable<AnalysisItemViewModel> editAnalysisItems { get; set; }
        public IEnumerable<AnalysisItemViewModel> delAnalysisItems { get; set; }

        public IEnumerable<PurchaseOrderItemViewModel> purchaseOrderItems { get; set; }
        public IEnumerable<PurchaseOrderItemViewModel> addPurchaseOrderItems { get; set; }
        public IEnumerable<PurchaseOrderItemViewModel> editPurchaseOrderItems { get; set; }
        public IEnumerable<PurchaseOrderItemViewModel> delPurchaseOrderItems { get; set; }
    }
}
