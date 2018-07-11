using System.Web.Http;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;

using Workflow.Domain.Entities.Finance;
using Workflow.Service;
using Workflow.Core.Attributes;
using System.Linq;
using Workflow.DataAcess.Repositories;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using Workflow.Domain.Entities.Forms;
using Workflow.Web.Models.HGVR;
using Workflow.Domain.Entities;
using Workflow.DataAcess.Repositories.Interfaces;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    [RoutePrefix("api/hgvrrequest")]
    public class HGVRRequestController : AbstractServiceController<HGVRRequestWorkflowInstance, HGVRRequestFormViewModel>
    {
        #region Business Form Logic
        protected override void MoreMapDataBC(HGVRRequestFormViewModel viewData, HGVRRequestWorkflowInstance workflowInstance)
        {
            // VoucherHotel Form Base
            workflowInstance.VoucherHotel = viewData.dataItem.voucherHotel.TypeAs<VoucherHotel>();

            // Voucher Details - Collection
            workflowInstance.AddVoucherHotelDetails = (from p in viewData.dataItem.addVoucherHotelDetails select p.TypeAs<VoucherHotelDetail>());
            workflowInstance.DelVoucherHotelDetails = (from p in viewData.dataItem.delVoucherHotelDetails select p.TypeAs<VoucherHotelDetail>());
            workflowInstance.EditVoucherHotelDetails = (from p in viewData.dataItem.editVoucherHotelDetails select p.TypeAs<VoucherHotelDetail>());

            // Finance Details - Collection
            workflowInstance.AddVoucherHotelFinances = (from p in viewData.dataItem.addVoucherHotelFinances select p.TypeAs<VoucherHotelFinance>());
            workflowInstance.DelVoucherHotelFinances = (from p in viewData.dataItem.delVoucherHotelFinances select p.TypeAs<VoucherHotelFinance>());
            workflowInstance.EditVoucherHotelFinances = (from p in viewData.dataItem.editVoucherHotelFinances select p.TypeAs<VoucherHotelFinance>());
        }

        protected override void MoreMapDataView(HGVRRequestWorkflowInstance workflowInstance, HGVRRequestFormViewModel viewData)
        {
            // Bind VoucherHotel data to view model
            viewData.dataItem.voucherHotel = workflowInstance.VoucherHotel.TypeAs<VoucherHotelViewModel>();

            // Cast and bind [Voucher Details] data list to view model
            if (workflowInstance.VoucherHotelDetails != null && workflowInstance.VoucherHotelDetails.Count() > 0)
            {
                viewData.dataItem.voucherHotelDetails = (from p in workflowInstance.VoucherHotelDetails select p.TypeAs<VoucherHotelDetailViewModel>());
            }

            // Cast and bind [Finance Details] data list to view model
            if (workflowInstance.VoucherHotelFinances != null && workflowInstance.VoucherHotelFinances.Count() > 0)
            {
                viewData.dataItem.voucherHotelFinances = (from p in workflowInstance.VoucherHotelFinances select p.TypeAs<VoucherHotelFinanceViewModel>());
            }
        }
        #endregion

        #region Constructor

        IDepartmentRepository _deptRepo;

        public HGVRRequestController()
        {
            _deptRepo = new DepartmentRepositoy();
        }
        // Initialize constructor of view model
        protected override HGVRRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new HGVRRequestFormViewModel();
        }

        protected override IRequestFormService<HGVRRequestWorkflowInstance> GetRequestformService()
        {
            return new HGVRRequestFormService();
        }
        #endregion

    }
}