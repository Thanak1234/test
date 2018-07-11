using System.Collections.Generic;

namespace Workflow.Web.Models.ITEIRQ
{
    public class ITEIRQRequestFormViewModel : AbstractFormDataViewModel
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
        public EventInternetViewModel eventInternet { get; set; }

        public IEnumerable<QuotationViewModel> quotations { get; set; }
        public IEnumerable<QuotationViewModel> addQuotations { get; set; }
        public IEnumerable<QuotationViewModel> editQuotations { get; set; }
        public IEnumerable<QuotationViewModel> delQuotations { get; set; }
    }
}
