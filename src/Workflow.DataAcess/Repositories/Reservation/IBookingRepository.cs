using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Reservation;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.DataAcess.Repositories.Reservation
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Booking GetByRequestHeader(int id);
        
        IEnumerable<RoomCategory> RoomCategories();

        string GetTermCondition();

        IEnumerable<OccupancyDto> ExecOccupacies(DateTime checkInDate, DateTime checkOutDate, int requestHeaderId, int occupancyId, double? occupancyVal, bool readOnly);

        int GetTotalRoomNightTaken(int requestHeaderId, DateTime checkInDate, DateTime checkOutDate,int numberOfRooms, int requestor);
        int GetTotalRoomNightTaken(int requestHeaderId);
    }
}
