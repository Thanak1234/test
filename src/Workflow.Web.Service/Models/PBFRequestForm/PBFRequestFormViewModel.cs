using System.Collections.Generic;

namespace Workflow.Web.Models.PBFRequestForm
{
    public class PBFRequestFormViewModel : AbstractFormDataViewModel
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

        public ProjectBriefViewModel projectBrief { get; set; }
        public IEnumerable<SpecItemViewModel> specifications { get; set; }
        public IEnumerable<SpecItemViewModel> addRequestItems { get; set; }
        public IEnumerable<SpecItemViewModel> editRequestItems { get; set; }
        public IEnumerable<SpecItemViewModel> delRequestItems { get; set; }
    }
}
