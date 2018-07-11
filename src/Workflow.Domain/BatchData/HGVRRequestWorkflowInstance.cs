
using System.Collections.Generic;
/**
*@author : Yim Samaune
*/
using Workflow.Domain.Entities.Forms;

namespace Workflow.Domain.Entities.BatchData
{
    public class HGVRRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public VoucherHotel VoucherHotel { get; set; }

        public IEnumerable<VoucherHotelDetail> VoucherHotelDetails { get; set; }
        public IEnumerable<VoucherHotelDetail> DelVoucherHotelDetails { get; set; }
        public IEnumerable<VoucherHotelDetail> EditVoucherHotelDetails { get; set; }
        public IEnumerable<VoucherHotelDetail> AddVoucherHotelDetails { get; set; }

        public IEnumerable<VoucherHotelFinance> VoucherHotelFinances { get; set; }
        public IEnumerable<VoucherHotelFinance> DelVoucherHotelFinances { get; set; }
        public IEnumerable<VoucherHotelFinance> EditVoucherHotelFinances { get; set; }
        public IEnumerable<VoucherHotelFinance> AddVoucherHotelFinances { get; set; }
        
    }
}