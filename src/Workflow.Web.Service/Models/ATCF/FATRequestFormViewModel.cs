using System.Collections.Generic;

namespace Workflow.Web.Models.ATCF
{
    public class ATCFRequestFormViewModel : AbstractFormDataViewModel
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
        public IEnumerable<AdditionalTimeWorkedViewModel> additionalTimeWorkeds { get; set; }
        public IEnumerable<AdditionalTimeWorkedViewModel> addAdditionalTimeWorkeds { get; set; }
        public IEnumerable<AdditionalTimeWorkedViewModel> editAdditionalTimeWorkeds { get; set; }
        public IEnumerable<AdditionalTimeWorkedViewModel> delAdditionalTimeWorkeds { get; set; }
    }
}
