using System.Collections.Generic;

namespace Workflow.Web.Models.HGVR
{
    public class HGVRRequestFormViewModel : AbstractFormDataViewModel
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
        public VoucherHotelViewModel voucherHotel { get; set; }

        public IEnumerable<VoucherHotelDetailViewModel> voucherHotelDetails { get; set; }
        public IEnumerable<VoucherHotelDetailViewModel> addVoucherHotelDetails { get; set; }
        public IEnumerable<VoucherHotelDetailViewModel> editVoucherHotelDetails { get; set; }
        public IEnumerable<VoucherHotelDetailViewModel> delVoucherHotelDetails { get; set; }

        public IEnumerable<VoucherHotelFinanceViewModel> voucherHotelFinances { get; set; }
        public IEnumerable<VoucherHotelFinanceViewModel> addVoucherHotelFinances { get; set; }
        public IEnumerable<VoucherHotelFinanceViewModel> editVoucherHotelFinances { get; set; }
        public IEnumerable<VoucherHotelFinanceViewModel> delVoucherHotelFinances { get; set; }
        
    }
}
