using System;
using System.Collections.Generic;
using Workflow.Business.BcjRequestForm;
using Workflow.Business.FnFRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Reservation;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class FnFRequestFormService : AbstractRequestFormService<IFnFRequestFormBC, FnFRequestWorkflowInstance>, IFnFRequestFormService
    {
        private IBookingRepository _bookingRepository = null;
        private IDbFactory workflow;

        public FnFRequestFormService()
        {
            workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC  = new FnFRequestFormBC(workflow, docWorkflow);
        }

        public int GetTotalRoomNightTaken(DateTime checkinDate, DateTime checkoutDate, int numberOfRoom, int requestorId)
        {
            _bookingRepository = new BookingRepository(workflow);
            return _bookingRepository.GetTotalRoomNightTaken(0, checkinDate, checkoutDate, numberOfRoom, requestorId);
        }

        public int GetTotalRoomNightTaken(int requestHeaderId)
        {
            _bookingRepository = new BookingRepository(workflow);
            return _bookingRepository.GetTotalRoomNightTaken(requestHeaderId);
        }
    }
}
