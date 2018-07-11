using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.DataObject.Reservation;
using Workflow.Domain.Entities.Reservation;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.Reservation;

namespace Workflow.Web.Service.Controllers
{
    public class FnFRequestController : AbstractServiceController<FnFRequestWorkflowInstance, FnFRequestFormViewModel>
    {
        private IFnFRequestFormService service;
        
        protected override FnFRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new FnFRequestFormViewModel();
        }
        
        protected override IRequestFormService<FnFRequestWorkflowInstance> GetRequestformService()
        {
            service = new FnFRequestFormService();
            return service;
        }
        
        public HttpResponseMessage GetTotalRoomNightTaken(DateTime checkinDate, DateTime checkoutDate, int numberOfRoom, int requestorId)
        {
            var totalRoomNightTaken = service.GetTotalRoomNightTaken(checkinDate, checkoutDate, numberOfRoom, requestorId);
            var result = new { success = true, value = totalRoomNightTaken };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public HttpResponseMessage GetTotalRoomNightTakenByHeader(int requestHeaderId)
        {
            var totalRoomNightTaken = service.GetTotalRoomNightTaken(requestHeaderId);
            var result = new { success = true, value = totalRoomNightTaken };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        protected override void MoreMapDataBC(FnFRequestFormViewModel viewData, FnFRequestWorkflowInstance workflowInstance)
        {
            workflowInstance.Reservation = viewData.dataItem.reservation.TypeAs<Booking>();
            
            workflowInstance.EditOccupancies = CreateOccupancies(viewData.dataItem.editoccupancies);
        }
        
        protected override void MoreMapDataView(FnFRequestWorkflowInstance workflowInstance, FnFRequestFormViewModel viewData)
        {
            viewData.dataItem.reservation = workflowInstance.Reservation.TypeAs<BookingViewModel>();

            var occupancies = new List<OccupancyViewModel>();
            if (workflowInstance.Occupancies != null)
            {
                foreach (var item in workflowInstance.Occupancies)
                {
                    var model = item.TypeAs<OccupancyViewModel>();
                    occupancies.Add(model);
                }
            }
            viewData.dataItem.occupancies = occupancies;
        }

        #region Private Method

        private IEnumerable<OccupancyDto> CreateOccupancies(IEnumerable<OccupancyViewModel> items)
        {
            var occupancies = new List<OccupancyDto>();
            if (items != null) {
                foreach (var item in items)
                {
                    occupancies.Add(item.TypeAs<OccupancyDto>());
                }
            }

            return occupancies;
        }

        #endregion
    }
}