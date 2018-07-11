using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject.Reservation;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.DataAcess.Repositories.Reservation
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public Booking GetByRequestHeader(int id)
        {
            IDbSet<Booking> dbSet = DbContext.Set<Booking>();
            try
            {
                return dbSet.Single(p => p.RequestHeaderId == id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

        public IEnumerable<OccupancyDto> ExecOccupacies(DateTime checkInDate, DateTime checkOutDate, int requestHeaderId, int occupancyId, double? occupancyVal, bool readOnly)
        {
            return SqlQuery<OccupancyDto>(@" EXEC [RESERVATION].[OCCUPANCY_DATE_LIST]	
                    @checkInDate = @checkInDate,
                    @checkOutDate = @checkOutDate,
                    @requestHeaderId = @requestHeaderId,
                    @occupancyId = @occupancyId,
                    @occupancyVal = @occupancyVal,
                    @readOnly = @readOnly",
                new {
                    checkInDate = checkInDate,
                    checkOutDate = checkOutDate,
                    requestHeaderId = requestHeaderId,
                    occupancyId = occupancyId,
                    occupancyVal = occupancyVal,
                    readOnly = readOnly
                });
        }

        public string GetTermCondition()
        {
            return GetText(@" SELECT [CONTENT] FROM [RESERVATION].[TERMS_CONDITIONS] WHERE [ACTIVE] = @active ", new { active = 1});
        }

        public int GetTotalRoomNightTaken(int requestHeaderId, DateTime checkInDate, DateTime checkOutDate, int numberOfRooms, int requestor)
        {
            return GetValue<int>(@" EXEC [RESERVATION].[NIGTH_TAKEN]	
                    @requestHeaderId = @requestHeaderId,
                    @checkInDate = @checkInDate,
                    @checkOutDate = @checkOutDate,
                    @numberOfRooms = @numberOfRooms,
                    @requestor = @requestor",
                new
                {
                    requestHeaderId = requestHeaderId,
                    checkInDate = checkInDate,
                    checkOutDate = checkOutDate,
                    numberOfRooms = numberOfRooms,
                    requestor = requestor
                });
        }

        public int GetTotalRoomNightTaken(int requestHeaderId)
        {
            return GetValue<int>(@" EXEC [RESERVATION].[NIGTH_TAKEN]	
                    @requestHeaderId = @requestHeaderId,
                    @checkInDate = null,
                    @checkOutDate = null,
                    @numberOfRooms = null,
                    @requestor = 0",
                new
                {
                    requestHeaderId = requestHeaderId
                });
        }

        public IEnumerable<RoomCategory> RoomCategories()
        {
            IDbSet<RoomCategory> dbSet = DbContext.Set<RoomCategory>();
            return dbSet.ToList();
        }
        
    }
}
