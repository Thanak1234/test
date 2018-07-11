using System.Collections.Generic;

namespace Workflow.Web.Models.Reservation
{
    public class FnFRequestFormViewModel : AbstractFormDataViewModel
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
        public BookingViewModel reservation { get; set; }

        public IEnumerable<OccupancyViewModel> occupancies { get; set; }
        public IEnumerable<OccupancyViewModel> addoccupancies { get; set; }
        public IEnumerable<OccupancyViewModel> editoccupancies { get; set; }
        public IEnumerable<OccupancyViewModel> deloccupancies { get; set; }
    }
}
