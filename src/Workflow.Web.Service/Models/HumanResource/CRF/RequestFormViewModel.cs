using System.Collections.Generic;

namespace Workflow.Web.Models.HumanResource.CRF
{
    public class RequestFormViewModel : AbstractFormDataViewModel
    {
        private ERFDataItem _dataItem;

        public ERFDataItem dataItem
        {
            get
            {
                return _dataItem ?? (_dataItem = new ERFDataItem());
            }
            set { _dataItem = value; }
        }
    }

    public class ERFDataItem
    {
        public RequisitionViewModel requisition { get; set; }
    }
}
