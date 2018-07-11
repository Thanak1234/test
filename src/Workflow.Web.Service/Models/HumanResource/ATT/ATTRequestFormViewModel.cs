/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Web.Models.HumanResource.ATT
{
    public class ATTRequestFormViewModel : AbstractFormDataViewModel
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
        public ATTTravelDetailViewModel travelDetail { get; set; }

        public IEnumerable<ATTDestinationViewModel> destinations { get; set; }
        public IEnumerable<ATTDestinationViewModel> addRequestDestinations { get; set; }
        public IEnumerable<ATTDestinationViewModel> editRequestDestinations { get; set; }
        public IEnumerable<ATTDestinationViewModel> delRequestDestinations { get; set; }

        public IEnumerable<ATTFlightDetailViewModel> flightDetails { get; set; }
        public IEnumerable<ATTFlightDetailViewModel> addRequestflightDetails { get; set; }
        public IEnumerable<ATTFlightDetailViewModel> editRequestflightDetails { get; set; }
        public IEnumerable<ATTFlightDetailViewModel> delRequestflightDetails { get; set; }
    }
}
