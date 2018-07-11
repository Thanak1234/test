using System.Collections.Generic;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.Web.Models.Reservation
{
    public class CRRRequestFormViewModel : AbstractFormDataViewModel
    {
        private CRRDataItem _dataItem;

        public CRRDataItem dataItem
        {
            get
            {
                return _dataItem ?? (_dataItem = new CRRDataItem());
            }
            set { _dataItem = value; }
        }
    }

    public class CRRDataItem
    {
        public ComplimentaryViewModel complimentary { get; set; }
        public IEnumerable<GuestViewModel>guests { get; set; }
        public IEnumerable<GuestViewModel> addRequestGuests { get; set; }
        public IEnumerable<GuestViewModel> editRequestGuests { get; set; }
        public IEnumerable<GuestViewModel> delRequestGuests { get; set; }
        public IEnumerable<ComplimentaryCheckItemViewModel> CheckExpenseItem { get; set; }
        public ComplimentaryCheckItemLS checkExpense { get; set; }
    }

}
